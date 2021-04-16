package com.activemesa.behavioral.command.exercise;

import org.junit.Test;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertEquals;

public class Evaluate
{
  @Test
  public void test()
  {
    Account a = new Account();

    Command command = new Command(Command.Action.DEPOSIT, 100);
    a.process(command);

    assertEquals(100, a.balance);
    assertTrue(command.success);

    command = new Command(Command.Action.WITHDRAW, 50);
    a.process(command);

    assertEquals(50, a.balance);
    assertTrue(command.success);

    command = new Command(Command.Action.WITHDRAW, 150);
    a.process(command);

    assertEquals(50, a.balance);
    assertFalse(command.success);
  }
}
