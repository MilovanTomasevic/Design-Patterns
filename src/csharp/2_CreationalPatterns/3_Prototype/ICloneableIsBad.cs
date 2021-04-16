using System;
using static System.Console;

namespace DotNetDesignPatternDemos.Creational.Prototype.ICloneableIsBad
{
  // ICloneable is ill-specified

  public class Address : ICloneable
  {
    public readonly string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
      StreetName = streetName;
      HouseNumber = houseNumber;
    }

    public override string ToString()
    {
      return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public object Clone()
    {
      return new Address(StreetName, HouseNumber);
    }
  }

  public class Person : ICloneable
  {
    public readonly string[] Names;
    public readonly Address Address;

    public Person(string[] names, Address address)
    {
      Names = names;
      Address = address;
    }

    public override string ToString()
    {
      return $"{nameof(Names)}: {string.Join(",", Names)}, {nameof(Address)}: {Address}";
    }

    public object Clone()
    {
      return new Person(Names, Address);
    }
  }

  public static class Demo
  {
    static void Main()
    {
      var john = new Person(new []{"John", "Smith"}, new Address("London Road", 123));

      var jane = (Person)john.Clone();
      jane.Address.HouseNumber = 321; // oops, John is now at 321

      // this doesn't work
      //var jane = john;

      // but clone is typically shallow copy
      jane.Names[0] = "Jane";

      WriteLine(john);
      WriteLine(jane);
    }
  }
}
