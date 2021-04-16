using System;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Behavioral.Command
{
  namespace Coding.Exercise
  {
    public class Command
    {
      public enum Action
      {
        Deposit,
        Withdraw
      }

      public Action TheAction;
      public int Amount;
      public bool Success;
    }

    public class Account
    {
      public int Balance { get; set; }

      public void Process(Command c)
      {
        switch (c.TheAction)
        {
          case Command.Action.Deposit:
            Balance += c.Amount;
            c.Success = true;
            break;
          case Command.Action.Withdraw:
            c.Success = Balance >= c.Amount;
            if (c.Success) Balance -= c.Amount;
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
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
        var a = new Account();

        var command = new Command{Amount = 100, TheAction = Command.Action.Deposit};
        a.Process(command);

        Assert.That(a.Balance, Is.EqualTo(100));
        Assert.IsTrue(command.Success);

        command = new Command{Amount = 50, TheAction = Command.Action.Withdraw};
        a.Process(command);

        Assert.That(a.Balance, Is.EqualTo(50));
        Assert.IsTrue(command.Success);

        command = new Command { Amount = 150, TheAction = Command.Action.Withdraw };
        a.Process(command);

        Assert.That(a.Balance, Is.EqualTo(50));
        Assert.IsFalse(command.Success);
      }
    }
  }
}