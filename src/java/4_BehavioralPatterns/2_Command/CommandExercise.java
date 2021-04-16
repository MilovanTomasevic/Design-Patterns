package com.activemesa.behavioral.command.exercise;

class Command
{
  enum Action
  {
    DEPOSIT, WITHDRAW
  }

  public Action action;
  public int amount;
  public boolean success;

  public Command(Action action, int amount)
  {
    this.action = action;
    this.amount = amount;
  }
}

class Account
{
  public int balance;

  public void process(Command c)
  {
    switch (c.action)
    {
      case DEPOSIT:
        balance += c.amount;
        c.success = true;
        break;
      case WITHDRAW:
        c.success = balance >= c.amount;
        if (c.success) balance -= c.amount;
        break;
    }
  }
}