using System.Collections.Generic;
using static System.Console;

namespace DotNetDesignPatternDemos.Behavioral.Memento.UndoRedo
{
  public class Memento
  {
    public int Balance { get; }

    public Memento(int balance)
    {
      Balance = balance;
    }
  }

  public class BankAccount
  {
    private int balance;
    private List<Memento> changes = new List<Memento>();
    private int current;

    public BankAccount(int balance)
    {
      this.balance = balance;
      changes.Add(new Memento(balance));
    }

    public Memento Deposit(int amount)
    {
      balance += amount;
      var m = new Memento(balance);
      changes.Add(m);
      ++current;
      return m;
    }

    public void Restore(Memento m)
    {
      if (m != null)
      {
        balance = m.Balance;
        changes.Add(m);
        current = changes.Count - 1;
      }
    }

    public Memento Undo()
    {
      if (current > 0)
      {
        var m = changes[--current];
        balance = m.Balance;
        return m;
      }
      return null;
    }

    public Memento Redo()
    {
      if (current + 1 < changes.Count)
      {
        var m = changes[++current];
        balance = m.Balance;
        return m;
      }
      return null;
    }

    public override string ToString()
    {
      return $"{nameof(balance)}: {balance}";
    }
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      var ba = new BankAccount(100);
      ba.Deposit(50);
      ba.Deposit(25);
      WriteLine(ba);

      ba.Undo();
      WriteLine($"Undo 1: {ba}");
      ba.Undo();
      WriteLine($"Undo 2: {ba}");
      ba.Redo();
      WriteLine($"Redo 2: {ba}");
    }
  }
}