using System.Runtime.InteropServices;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Behavioral.TemplateMethod
{
  namespace Coding.Exercise
  {
    public class Creature
    {
      public int Attack, Health;

      public Creature(int attack, int health)
      {
        Attack = attack;
        Health = health;
      }
    }

    public abstract class CardGame
    {
      public Creature[] Creatures;

      public CardGame(Creature[] creatures)
      {
        Creatures = creatures;
      }

      // returns -1 if no clear winner (both alive or both dead)
      public int Combat(int creature1, int creature2)
      {
        Creature first = Creatures[creature1];
        Creature second = Creatures[creature2];
        Hit(first, second);
        Hit(second, first);
        bool firstAlive = first.Health > 0;
        bool secondAlive = second.Health > 0;
        if (firstAlive == secondAlive) return -1;
        return firstAlive ? creature1 : creature2;
      }

      // attacker hits other creature
      protected abstract void Hit(Creature attacker, Creature other);
    }

    public class TemporaryCardDamageGame : CardGame
    {
      public TemporaryCardDamageGame(Creature[] creatures) : base(creatures)
      {
      }

      protected override void Hit(Creature attacker, Creature other)
      {
        var oldHealth = other.Health;
        other.Health -= attacker.Attack;
        if (other.Health > 0)
          other.Health = oldHealth;
      }
    }

    public class PermanentCardDamage : CardGame
    {
      public PermanentCardDamage(Creature[] creatures) : base(creatures)
      {
      }

      protected override void Hit(Creature attacker, Creature other)
      {
        other.Health -= attacker.Attack;
      }
    }
  }

  namespace Coding.Exercise.Tests
  {
    [TestFixture]
    public class Tests
    {
      [Test]
      public void ImpasseTest()
      {
        var c1 = new Creature(1,2);
        var c2 = new Creature(1, 2);
        CardGame game = new TemporaryCardDamageGame(new []{c1, c2});
        Assert.That(game.Combat(0, 1), Is.EqualTo(-1));
        Assert.That(game.Combat(0, 1), Is.EqualTo(-1));
      }

      [Test]
      public void TemporaryMurderTest()
      {
        var c1 = new Creature(1,1);
        var c2 = new Creature(2,2);
        CardGame game = new TemporaryCardDamageGame(new[]{c1, c2});
        Assert.That(game.Combat(0,1), Is.EqualTo(1));
      }

      [Test]
      public void DoubleMurderTest()
      {
        var c1 = new Creature(2, 2);
        var c2 = new Creature(2, 2);
        CardGame game = new TemporaryCardDamageGame(new[] { c1, c2 });
        Assert.That(game.Combat(0, 1), Is.EqualTo(-1));
      }

      [Test]
      public void PermanentDamageDeathTest()
      {
        var c1 = new Creature(1, 2);
        var c2 = new Creature(1, 3);
        CardGame game = new PermanentCardDamage(new[] { c1, c2 });
        Assert.That(game.Combat(0, 1), Is.EqualTo(-1));
        Assert.That(game.Combat(0, 1), Is.EqualTo(1));
      }
    }
  }
}