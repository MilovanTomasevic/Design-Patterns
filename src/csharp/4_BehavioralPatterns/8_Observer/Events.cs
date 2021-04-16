using System;

namespace DotNetDesignPatternDemos.Behavioral.Observer
{
  public class FallsIllEventArgs
  {
    public string Address;
  }

  public class Person
  {
    public void CatchACold()
    {
      FallsIll?.Invoke(this,
        new FallsIllEventArgs { Address = "123 London Road" });
    }

    public event EventHandler<FallsIllEventArgs> FallsIll;
  }

  public class Demo
  {
    static void Main()
    {
      var person = new Person();

      person.FallsIll += CallDoctor;

      person.CatchACold();
    }

    private static void CallDoctor(object sender, FallsIllEventArgs eventArgs)
    {
      Console.WriteLine($"A doctor has been called to {eventArgs.Address}");
    }
  }
}