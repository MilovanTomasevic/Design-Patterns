using NUnit.Framework;

namespace DotNetDesignPatternDemos.Behavioral.State
{
  namespace Coding.Exercise
  {
    public class CombinationLock
    {
      private readonly int[] combination;

      public CombinationLock(int [] combination)
      {
        this.combination = combination;
        Reset();
      }

      private void Reset()
      {
        Status = "LOCKED";
        digitsEntered = 0;
        failed = false;
      }

      public string Status;

      private int digitsEntered = 0;
      private bool failed = false;

      public void EnterDigit(int digit)
      {
        if (Status == "LOCKED") Status = string.Empty;
        Status += digit.ToString();
        if (combination[digitsEntered] != digit)
        {
          failed = true;
        }
        digitsEntered++;

        if (digitsEntered == combination.Length)
          Status = failed ? "ERROR" : "OPEN";
      }
    }
  }

  namespace Coding.Exercise.Tests
  {
    [TestFixture]
    public class Tests
    {
      [Test]
      public void TestSuccess()
      {
        var cl = new CombinationLock(new[] {1, 2, 3, 4, 5});
        Assert.That(cl.Status, Is.EqualTo("LOCKED"));
        cl.EnterDigit(1);
        Assert.That(cl.Status, Is.EqualTo("1"));
        cl.EnterDigit(2);
        Assert.That(cl.Status, Is.EqualTo("12"));
        cl.EnterDigit(3);
        Assert.That(cl.Status, Is.EqualTo("123"));
        cl.EnterDigit(4);
        Assert.That(cl.Status, Is.EqualTo("1234"));
        cl.EnterDigit(5);
        Assert.That(cl.Status, Is.EqualTo("OPEN"));
      }

      [Test]
      public void TestFailure()
      {
        var cl = new CombinationLock(new[]{1,2,3});
        Assert.That(cl.Status, Is.EqualTo("LOCKED"));
        cl.EnterDigit(1);
        Assert.That(cl.Status, Is.EqualTo("1"));
        cl.EnterDigit(2);
        Assert.That(cl.Status, Is.EqualTo("12"));
        cl.EnterDigit(5);
        Assert.That(cl.Status, Is.EqualTo("ERROR"));
      }
    }
  }
}