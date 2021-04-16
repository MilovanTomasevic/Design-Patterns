using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

// todo: fix namespace
namespace DesignPatterns
{
  public class BankAccount
  {
    private int balance;
    private int overdraftLimit = -500;

    public void Deposit(int amount)
    {
      balance += amount;
      WriteLine($"Deposited ${amount}, balance is now {balance}");
    }

    public bool Withdraw(int amount)
    {
      if (balance - amount >= overdraftLimit)
      {
        balance -= amount;
        WriteLine($"Withdrew ${amount}, balance is now {balance}");
        return true;
      }
      return false;
    }

    public override string ToString()
    {
      return $"{nameof(balance)}: {balance}";
    }
  }

  public interface ICommand
  {
    void Call();
    void Undo();
  }

  public class BankAccountCommand : ICommand
  {
    private BankAccount account;

    public enum Action
    {
      Deposit, Withdraw
    }

    private Action action;
    private int amount;
    private bool succeeded;

    public BankAccountCommand(BankAccount account, Action action, int amount)
    {
      this.account = account ?? throw new ArgumentNullException(paramName: nameof(account));
      this.action = action;
      this.amount = amount;
    }

    public void Call()
    {
      switch (action)
      {
        case Action.Deposit:
          account.Deposit(amount);
          succeeded = true;
          break;
        case Action.Withdraw:
          succeeded = account.Withdraw(amount);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void Undo()
    {
      if (!succeeded) return;
      switch (action)
      {
        case Action.Deposit:
          account.Withdraw(amount);
          break;
        case Action.Withdraw:
          account.Deposit(amount);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }

  class Demo
  {
    static void Main(string[] args)
    {
      var ba = new BankAccount();
      var commands = new List<BankAccountCommand>
      {
        new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
        new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 1000)
      };

      WriteLine(ba);

      foreach (var c in commands)
        c.Call();

      WriteLine(ba);

      foreach (var c in Enumerable.Reverse(commands))
        c.Undo();

      WriteLine(ba);
    }
  }
}