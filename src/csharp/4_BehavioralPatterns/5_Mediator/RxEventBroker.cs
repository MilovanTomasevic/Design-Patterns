using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Autofac;

namespace RxDemos.ImplementingObservable.Broker
{
  public class Actor
  {
    protected EventBroker broker;

    public Actor(EventBroker broker)
    {
      this.broker = broker ?? throw new ArgumentNullException(paramName: nameof(broker));
    }
  }

  public class FootballCoach : Actor
  {
    public FootballCoach(EventBroker broker) : base(broker)
    {
      broker.OfType<PlayerScoredEvent>()
        .Subscribe(
          ps =>
          {
            if (ps.GoalsScored < 3)
              Console.WriteLine($"Coach: well done, {ps.Name}!");
          }
        );

      broker.OfType<PlayerSentOffEvent>()
        .Subscribe(
          ps =>
          {
            if (ps.Reason == "violence")
              Console.WriteLine($"Coach: How could you, {ps.Name}?");
          });
    }
  }

  public class Ref : Actor
  {
    public Ref(EventBroker broker) : base(broker)
    {
      broker.OfType<PlayerEvent>()
        .Subscribe(e =>
        {
          if (e is PlayerScoredEvent scored)
            Console.WriteLine($"REF: player {scored.Name} has scored his {scored.GoalsScored} goal.");
          if (e is PlayerSentOffEvent sentOff)
            Console.WriteLine($"REF: player {sentOff.Name} sent off due to {sentOff.Reason}.");
        });
    }
  }

  public class FootballPlayer : Actor
  {
    private IDisposable sub;
    public string Name { get; set; } = "Unknown Player";
    public int GoalsScored { get; set; } = 0;

    public void Score()
    {
      GoalsScored++;
      broker.Publish(new PlayerScoredEvent {Name = Name, GoalsScored = GoalsScored});
    }

    public void AssaultReferee()
    {
      broker.Publish(new PlayerSentOffEvent {Name = Name, Reason = "violence"});

    }

    public FootballPlayer(EventBroker broker, string name) : base(broker)
    {
      if (name == null)
      {
        throw new ArgumentNullException(paramName: nameof(name));
      }
      Name = name;

      broker.OfType<PlayerScoredEvent>()
        .Where(ps => !ps.Name.Equals(name))
        .Subscribe(ps => Console.WriteLine($"{name}: Nicely scored, {ps.Name}! It's your {ps.GoalsScored} goal!"));

      sub = broker.OfType<PlayerSentOffEvent>()
        .Where(ps => !ps.Name.Equals(name))
        .Subscribe(ps => Console.WriteLine($"{name}: See you in the lockers, {ps.Name}."));
    }
  }

  public class PlayerEvent
  {
    public string Name { get; set; }
  }

  public class PlayerScoredEvent : PlayerEvent
  {
    public int GoalsScored { get; set; }
  }

  public class PlayerSentOffEvent : PlayerEvent
  {
    public string Reason { get; set; }
  }

  public class EventBroker : IObservable<PlayerEvent>
  {
    private readonly Subject<PlayerEvent> subscriptions = new Subject<PlayerEvent>();
    public IDisposable Subscribe(IObserver<PlayerEvent> observer)
    {
      return subscriptions.Subscribe(observer);
    }

    public void Publish(PlayerEvent pe)
    {
      subscriptions.OnNext(pe);
    }
  }

  public class Demo
  {
    static void MainB(string[] args)
    {
      var cb = new ContainerBuilder();
      cb.RegisterType<EventBroker>().SingleInstance();
      cb.RegisterType<FootballCoach>();
      cb.RegisterType<Ref>();
      cb.Register((c, p) => new FootballPlayer(c.Resolve<EventBroker>(), p.Named<string>("name")));

      using (var c = cb.Build())
      {
        var referee = c.Resolve<Ref>(); // order matters here!
        var coach = c.Resolve<FootballCoach>();
        var player1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "John"));
        var player2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));
        player1.Score();
        player1.Score();
        player1.Score(); // only 2 notifications
        player1.AssaultReferee();
        player2.Score();
      }
    }
  }
}