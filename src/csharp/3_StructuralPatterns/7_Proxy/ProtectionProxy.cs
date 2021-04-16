using System;
using Autofac;
using static System.Console;

namespace DotNetDesignPatternDemos.Structural.Proxy.Protection
{
  public interface ICar
  {
    void Drive();
  }

  public class Car : ICar
  {
    public void Drive()
    {
      WriteLine("Car being driven");
    }
  }

  public class CarProxy : ICar
  {
    private Car car = new Car();
    private Driver driver;

    public CarProxy(Driver driver)
    {
      this.driver = driver;
    }

    public void Drive()
    {
      if (driver.Age >= 16)
        car.Drive();
      else
      {
        WriteLine("Driver too young");
      }
    }
  }

  public class Driver
  {
    public int Age { get; set; }
    
    public Driver(int age)
    {
      Age = age;
    }
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      ICar car = new CarProxy(new Driver(12)); // 22
      car.Drive();
    }
  }
}