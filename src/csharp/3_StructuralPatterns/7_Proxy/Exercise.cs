using NUnit.Framework;

namespace DotNetDesignPatternDemos.Structural.Proxy
{
  namespace Coding.Exercise
  {
    public class Person
    {
      public int Age { get; set; }

      public string Drink()
      {
        return "drinking";
      }

      public string Drive()
      {
        return "driving";
      }

      public string DrinkAndDrive()
      {
        return "driving while drunk";
      }
    }

    public class ResponsiblePerson
    {
      private readonly Person person;

      public ResponsiblePerson(Person person)
      {
        this.person = person;
      }

      public int Age
      {
        get { return person.Age; }
        set { person.Age = value; }
      }

      public string Drink()
      {
        if (Age >= 18)
          return person.Drink();
        return "too young";
      }

      public string Drive()
      {
        if (Age >= 16)
          return person.Drive();
        return "too young";
      }

      public string DrinkAndDrive()
      {
        return "dead";
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
        var p = new Person {Age = 10};
        var rp = new ResponsiblePerson(p);

        Assert.That(rp.Drive(), Is.EqualTo("too young"));
        Assert.That(rp.Drink(), Is.EqualTo("too young"));
        Assert.That(rp.DrinkAndDrive(), Is.EqualTo("dead"));

        rp.Age = 20;

        Assert.That(rp.Drive(), Is.EqualTo("driving"));
        Assert.That(rp.Drink(), Is.EqualTo("drinking"));
        Assert.That(rp.DrinkAndDrive(), Is.EqualTo("dead"));
      }
    }
  }
}