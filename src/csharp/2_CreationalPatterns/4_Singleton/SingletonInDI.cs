using System;
using System.Diagnostics;
using Autofac;
using static System.Console;

namespace DotNetDesignPatternDemos.Creational.SingletonInDI
{
  public class Foo
  {
    public EventBroker Broker;

    public Foo(EventBroker broker)
    {
      Broker = broker ?? throw new ArgumentNullException(paramName: nameof(broker));
    }
  }

  public class EventBroker
  {
    
  }

  // socially acceptable 
  public class SingletonDI
  {
    static void Main()
    {
      var builder = new ContainerBuilder();
      builder.RegisterType<EventBroker>().SingleInstance();
      builder.RegisterType<Foo>();

      using (var c = builder.Build())
      {
        var foo1 = c.Resolve<Foo>();
        var foo2 = c.Resolve<Foo>();
        
        WriteLine(ReferenceEquals(foo1, foo2));
        WriteLine(ReferenceEquals(foo1.Broker, foo2.Broker));
      }

    }
  }
}