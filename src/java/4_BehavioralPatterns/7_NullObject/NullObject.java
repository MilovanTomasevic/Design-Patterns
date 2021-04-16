package com.activemesa.behavioral.nullobject;

interface Log
{
  void info(String msg);
  void warn(String msg);
}

class ConsoleLog implements Log
{

  @Override
  public void info(String msg)
  {
    System.out.println(msg);
  }

  @Override
  public void warn(String msg)
  {
    System.out.println("WARNING: " + msg);
  }
}

class BankAccount
{
  private Log log;
  private int balance;

  public BankAccount(Log log)
  {
    this.log = log;
  }

  public void deposit(int amount)
  {
    balance += amount;

    // check for null everywhere?
    if (log != null)
    {
      log.info("Deposited " + amount
        + ", balance is now " + balance);
    }
  }

  public void withdraw(int amount)
  {
    if (balance >= amount)
    {
      balance -= amount;
      if (log != null)
      {
        System.out.println("Withdrew " + amount
          + ", we have " + balance + " left");
      }
    }
    else {
      if (log != null)
      {
        System.out.println("Could not withdraw "
          + amount + " because balance is only " + balance);
      }
    }
  }
}

final class NullLog implements Log
{

  @Override
  public void info(String msg)
  {

  }

  @Override
  public void warn(String msg)
  {

  }
}

class NullObjectDemo
{
  public static void main(String[] args)
  {
    //ConsoleLog log = new ConsoleLog();
    //Log log = null;
    NullLog log = new NullLog();

    BankAccount ba = new BankAccount(log);
    ba.deposit(100);
    ba.withdraw(200);
  }
}