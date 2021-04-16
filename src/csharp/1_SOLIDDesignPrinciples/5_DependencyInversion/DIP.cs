using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace DotNetDesignPatternDemos.SOLID.DependencyInversionPrinciple
{
  // hl modules should not depend on low-level; both should depend on abstractions
  // abstractions should not depend on details; details should depend on abstractions

  public enum Relationship
  {
    Parent,
    Child,
    Sibling
  }

  public class Person
  {
    public string Name;
    // public DateTime DateOfBirth;
  }

  public interface IRelationshipBrowser
  {
    IEnumerable<Person> FindAllChildrenOf(string name);
  }

  public class Relationships : IRelationshipBrowser // low-level
  {
    private List<(Person,Relationship,Person)> relations
      = new List<(Person, Relationship, Person)>();

    public void AddParentAndChild(Person parent, Person child)
    {
      relations.Add((parent, Relationship.Parent, child));
      relations.Add((child, Relationship.Child, parent));
    }

    public List<(Person, Relationship, Person)> Relations => relations;

    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
      return relations
        .Where(x => x.Item1.Name == name
                    && x.Item2 == Relationship.Parent).Select(r => r.Item3);
    }
  }

  public class Research
  {
    public Research(Relationships relationships) 
    {
      // high-level: find all of john's children
      //var relations = relationships.Relations;
      //foreach (var r in relations
      //  .Where(x => x.Item1.Name == "John"
      //              && x.Item2 == Relationship.Parent))
      //{
      //  WriteLine($"John has a child called {r.Item3.Name}");
      //}
    }

    public Research(IRelationshipBrowser browser) {
      foreach (var p in browser.FindAllChildrenOf("John"))
      {
        WriteLine($"John has a child called {p.Name}");
      }
    }

    static void Main(string[] args)
    {
      var parent = new Person {Name = "John"};
      var child1 = new Person {Name = "Chris"};
      var child2 = new Person {Name = "Matt"};

      // low-level module
      var relationships = new Relationships();
      relationships.AddParentAndChild(parent, child1);
      relationships.AddParentAndChild(parent, child2);

      new Research(relationships);
      
    }
  }
}
