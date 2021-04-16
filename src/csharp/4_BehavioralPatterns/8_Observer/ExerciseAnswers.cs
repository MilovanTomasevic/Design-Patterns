using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Behavioral.Observer
{
  namespace Coding.Exercise
  {
    public class Game
    {
      public event EventHandler RatEnters, RatDies;
      public event EventHandler<Rat> NotifyRat;
  
      public void FireRatEnters(object sender)
      {
        RatEnters?.Invoke(sender, EventArgs.Empty);
      }
  
      public void FireRatDies(object sender)
      {
        RatDies?.Invoke(sender, EventArgs.Empty);
      }
  
      public void FireNotifyRat(object sender, Rat whichRat)
      {
        NotifyRat?.Invoke(sender, whichRat);
      }
    }
  
    public class Rat : IDisposable
    {
      private readonly Game game;
      public int Attack = 1;
      
      public Rat(Game game)
      {
        this.game = game;
        game.RatEnters += (sender, args) =>
        {
          if (sender != this)
          {
            ++Attack;
            game.FireNotifyRat(this, (Rat)sender);
          }
        };
        game.NotifyRat += (sender, rat) =>
        {
          if (rat == this) ++Attack;
        };
        game.RatDies += (sender, args) => --Attack;
        game.FireRatEnters(this);
      }
  
  
      public void Dispose()
      {
        game.FireRatDies(this);
      }
    }
  }

  namespace Coding.Exercise.Tests
  {
    [TestFixture]
    public class Tests
    {
      [Test]
      public void PlayingByTheRules()
      {
        Assert.That(typeof(Game).GetFields(), Is.Empty);
        Assert.That(typeof(Game).GetProperties(), Is.Empty);
      }
      
      [Test]
      public void SingleRatTest()
      {
        var game = new Game();
        var rat = new Rat(game);
        Assert.That(rat.Attack, Is.EqualTo(1));
      }
      
      [Test]
      public void TwoRatTest()
      {
        var game = new Game();
        var rat = new Rat(game);
        var rat2 = new Rat(game);
        Assert.That(rat.Attack, Is.EqualTo(2));
        Assert.That(rat2.Attack, Is.EqualTo(2));
      }
  
      [Test]
      public void ThreeRatsOneDies()
      {
        var game = new Game();
        
        var rat = new Rat(game);
        Assert.That(rat.Attack, Is.EqualTo(1));
        
        var rat2 = new Rat(game);
        Assert.That(rat.Attack, Is.EqualTo(2));
        Assert.That(rat2.Attack, Is.EqualTo(2));
  
        using (var rat3 = new Rat(game))
        {
          Assert.That(rat.Attack, Is.EqualTo(3));
          Assert.That(rat2.Attack, Is.EqualTo(3));
          Assert.That(rat3.Attack, Is.EqualTo(3));
        }
        
        Assert.That(rat.Attack, Is.EqualTo(2));
        Assert.That(rat2.Attack, Is.EqualTo(2));
      }
    }
  }
}