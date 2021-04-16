using System;
using static System.Console;

namespace DotNetDesignPatternDemos.Creational.Prototype.Inheritance
{
  public interface IDeepCopyable<T> where T : new()
  {
    void CopyTo(T target);
    
    public T DeepCopy()
    {
      T t = new T();
      CopyTo(t);
      return t;
    }
  }
  
  public class Address : IDeepCopyable<Address>
  {
    public string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
      StreetName = streetName;
      HouseNumber = houseNumber;
    }

    public Address()
    {
      
    }

    public override string ToString()
    {
      return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public void CopyTo(Address target)
    {
      target.StreetName = StreetName;
      target.HouseNumber = HouseNumber;
    }
  }



  public class Person : IDeepCopyable<Person>
  {
    public string[] Names;
    public Address Address;

    public Person()
    {
      
    }
    
    public Person(string[] names, Address address)
    {
      Names = names;
      Address = address;
    }

    public override string ToString()
    {
      return $"{nameof(Names)}: {string.Join(",", Names)}, {nameof(Address)}: {Address}";
    }

    public virtual void CopyTo(Person target)
    {
      target.Names = (string[]) Names.Clone();
      target.Address = Address.DeepCopy();
    }
  }

  public class Employee : Person, IDeepCopyable<Employee>
  {
    public int Salary;

    public void CopyTo(Employee target)
    {
      base.CopyTo(target);
      target.Salary = Salary;
    }

    public override string ToString()
    {
      return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
    }
  }
  
  public static class DeepCopyExtensions
  {
    public static T DeepCopy<T>(this IDeepCopyable<T> item) 
      where T : new()
    {
      return item.DeepCopy();
    }

    public static T DeepCopy<T>(this T person)
      where T : Person, new()
    {
      return ((IDeepCopyable<T>) person).DeepCopy();
    }
  }
  
  public static class Demo
  {
    static void Main()
    {
      var john = new Employee();
      john.Names = new[] {"John", "Doe"};
      john.Address = new Address {HouseNumber = 123, StreetName = "London Road"};
      john.Salary = 321000;
      var copy = john.DeepCopy();

      copy.Names[1] = "Smith";
      copy.Address.HouseNumber++;
      copy.Salary = 123000;
      
      Console.WriteLine(john);
      Console.WriteLine(copy);
    }
  }
}