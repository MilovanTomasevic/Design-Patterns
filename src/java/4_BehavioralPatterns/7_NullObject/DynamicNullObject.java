package com.activemesa.behavioral.nullobject.dynamic;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Proxy;


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
        log.info("Withdrew " + amount
          + ", we have " + balance + " left");
      }
    }
    else {
      if (log != null)
      {
        log.warn("Could not withdraw "
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

class Demo
{
  @SuppressWarnings("unchecked")
  public static <T> T noOp(Class<T> itf)
  {
    return (T) Proxy.newProxyInstance(
      itf.getClassLoader(),
      new Class<?>[]{itf},
      (proxy, method, args) ->
      {
        if (method.getReturnType().equals(Void.TYPE))
          return null;
        else
          return method.getReturnType().getConstructor().newInstance();
      });
  }

  public static void main(String[] args)
  {
    //ConsoleLog log = new ConsoleLog();
    //NullLog log = new NullLog();

    Log log = noOp(Log.class);

    BankAccount ba = new BankAccount(log);
    ba.deposit(100);
    ba.withdraw(200);
  }
}