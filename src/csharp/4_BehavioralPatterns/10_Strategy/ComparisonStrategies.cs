using System;
using System.Collections.Generic;
using System.Linq;

// todo: this is somehow buggy in recording
namespace DotNetDesignPatternDemos.Behavioral.Strategy
{
  class Person : IEquatable<Person>, IComparable<Person>
  {
    public int Id;
    public string Name;
    public int Age;

    public int CompareTo(Person other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      return Id.CompareTo(other.Id);
    }

    public Person(int id, string name, int age)
    {
      Id = id;
      Name = name;
      Age = age;
    }

    public bool Equals(Person other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Person) obj);
    }

    public override int GetHashCode()
    {
      return Id;
    }

    public static bool operator ==(Person left, Person right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Person left, Person right)
    {
      return !Equals(left, right);
    }

    private sealed class NameRelationalComparer : IComparer<Person>
    {
      public int Compare(Person x, Person y)
      {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return string.Compare(x.Name, y.Name, 
          StringComparison.Ordinal);
      }
    }

    public static IComparer<Person> NameComparer { get; } 
      = new NameRelationalComparer();
  }
  
  public class ComparisonStrategies
  {
    public static void Main(string[] args)
    {
      var people = new List<Person>();
      
      // equality == != and comparison < = >
      
      people.Sort(); // meaningless by default
      
      // sort by name with a lambda
      people.Sort((x, y) => x.Name.CompareTo(y.Name));
      
      people.Sort(Person.NameComparer);

    }
  }
}