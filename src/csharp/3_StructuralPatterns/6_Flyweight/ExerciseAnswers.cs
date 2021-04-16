using System.Collections.Generic;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Structural.Flyweight
{
  namespace Coding.Exercise
  {
    public class Sentence
    {
      private string[] words;
      private Dictionary<int, WordToken> tokens = new Dictionary<int, WordToken>();

      public Sentence(string plainText)
      {
        words = plainText.Split(' ');
      }

      public WordToken this[int index]
      {
        get
        {
          WordToken wt = new WordToken();
          tokens.Add(index, wt);
          return tokens[index];
        }
      }

      public override string ToString()
      {
        var ws = new List<string>();
        for (var i = 0; i < words.Length; i++)
        {
          var w = words[i];
          if (tokens.ContainsKey(i) && tokens[i].Capitalize)
            w = w.ToUpperInvariant();
          ws.Add(w);
        }
        return string.Join(" ", ws);
      }

      public class WordToken
      {
        public bool Capitalize;
      }
    }
  }

  namespace Coding.Exercise.Tests
  {
    [TestFixture]
    public class TestSuite
    {
      [Test]
      public void Test()
      {
        var s = new Sentence("alpha beta gamma");
        s[1].Capitalize = true;
        Assert.That(s.ToString(),
          Is.EqualTo("alpha BETA gamma"));
      }
    }
  }
}