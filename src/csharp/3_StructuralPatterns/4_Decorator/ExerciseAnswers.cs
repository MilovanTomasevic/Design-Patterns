using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DotNetDesignPatternDemos.Structural.Decorator
{
  namespace Coding.Exercise
  {
    public class Bird
    {
      public int Age { get; set; }
      
      public string Fly()
      {
        return (Age < 10) ? "flying" : "too old";
      }
    }

    public class Lizard
    {
      public int Age { get; set; }
      
      public string Crawl()
      {
        return (Age > 1) ? "crawling" : "too young";
      }
    }

    public class Dragon // no need for interfaces
    {
      private int age;
      private Bird bird = new Bird();
      private Lizard lizard = new Lizard();

      public int Age
      {
        set { age = bird.Age = lizard.Age = value; }
        get { return age; }
      }

      public string Fly()
      {
        return bird.Fly();
      }

      public string Crawl()
      {
        return lizard.Crawl();
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
        var dragon = new Dragon();

        Assert.That(dragon.Fly(), Is.EqualTo("flying"));
        Assert.That(dragon.Crawl(), Is.EqualTo("too young"));

        dragon.Age = 20;

        Assert.That(dragon.Fly(), Is.EqualTo("too old"));
        Assert.That(dragon.Crawl(), Is.EqualTo("crawling"));
      }
    }
  }
}