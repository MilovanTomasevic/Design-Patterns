using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Behavioral.Memento.Exercise
{
  namespace Coding.Exercise
  {
    public class Token
    {
      public int Value = 0;
  
      public Token(int value)
      {
        Value = value;
      }
    }
  
    public class Memento
    {
      public List<Token> Tokens = new List<Token>();
    }
  
    public class TokenMachine
    {
      public List<Token> Tokens = new List<Token>();
  
      public Memento AddToken(int value)
      {
        return AddToken(new Token(value));
      }
  
      public Memento AddToken(Token token)
      {
        Tokens.Add(token);
        var m = new Memento();
        // a rather roundabout way of cloning
        m.Tokens = Tokens.Select(t => new Token(t.Value)).ToList();
        return m;
      }
  
      public void Revert(Memento m)
      {
        Tokens = m.Tokens.Select(mm => new Token(mm.Value)).ToList();
      }
    }
  }

  namespace Coding.Exercise.Tests
  {
    [TestFixture]
    public class Tests
    {
      [Test]
      public void SingleTokenTest()
      {
        var tm = new TokenMachine();
        var m = tm.AddToken(123);
        tm.AddToken(456);
        tm.Revert(m);
        Assert.That(tm.Tokens.Count, Is.EqualTo(1));
        Assert.That(tm.Tokens[0].Value, Is.EqualTo(123));
      }
  
      [Test]
      public void TwoTokenTest()
      {
        var tm = new TokenMachine();
        tm.AddToken(1);
        var m = tm.AddToken(2);
        tm.AddToken(3);
        tm.Revert(m);
        Assert.That(tm.Tokens.Count, Is.EqualTo(2));
        Assert.That(tm.Tokens[0].Value, Is.EqualTo(1), 
          $"First token should have value 1, you got {tm.Tokens[0].Value}");
        Assert.That(tm.Tokens[1].Value, Is.EqualTo(2));
      }
  
      [Test]
      public void FiddledTokenTest()
      {
        var tm = new TokenMachine();
        Console.WriteLine("Made a token with value 111 and kept a reference");
        var token = new Token(111);
        Console.WriteLine("Added this token to the list");
        tm.AddToken(token);
        var m = tm.AddToken(222);
        Console.WriteLine("Changed this token's value to 333 :)");
        token.Value = 333;
        tm.Revert(m);
        
        Assert.That(tm.Tokens.Count, Is.EqualTo(2),
          $"At this point, token machine should have exactly two tokens, you got {tm.Tokens.Count}");
        
        Assert.That(tm.Tokens[0].Value, Is.EqualTo(111),
          $"You got the token value wrong here. Hint: did you init the memento by value?");
      }
    }
  }
}