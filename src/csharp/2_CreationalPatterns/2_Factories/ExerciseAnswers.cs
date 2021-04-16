using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Creational.Factories
{
  namespace Coding.Exercise
  {
    public class Person
    {
      public int Id { get; set; }
      public string Name { get; set; }
    }

    public class PersonFactory
    {
      private int id = 0;

      public Person CreatePerson(string name)
      {
        return new Person {Id = id++, Name = name};
      }
    }
  }

  namespace Coding.Exercise.UnitTests
  {
    [TestFixture]
    public class FirstTestSuite
    {
      [Test]
      public void Test()
      {
        var pf = new PersonFactory();

        var p1 = pf.CreatePerson("Chris");
        Assert.That(p1.Name, Is.EqualTo("Chris"));
        Assert.That(p1.Id, Is.EqualTo(0));

        var p2 = pf.CreatePerson("Sarah");
        Assert.That(p2.Id, Is.EqualTo(1));
      }
    }
  }
}