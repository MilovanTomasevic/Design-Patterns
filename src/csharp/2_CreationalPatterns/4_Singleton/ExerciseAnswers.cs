using System;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Creational.Singleton
{
  namespace Coding.Exercise
  {
    public class SingletonTester
    {
      public static bool IsSingleton(Func<object> func)
      {
        var obj1 = func();
        var obj2 = func();
        return ReferenceEquals(obj1, obj2);
      }
    }
  }

  namespace Coding.Exercise.Tests
  {
    [TestFixture]
    public class FirstTestSuite
    {
      [Test]
      public void Test()
      {
        var obj = new object();
        Assert.IsTrue(SingletonTester.IsSingleton(() => obj));
        Assert.IsFalse(SingletonTester.IsSingleton(() => new object()));
      }
    }
  }
}