package com.activemesa.behavioral.mediator.exercise;

import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class Evaluate
{
  @Test
  public void test()
  {
    Mediator mediator = new Mediator();
    Participant p1 = new Participant(mediator);
    Participant p2 = new Participant(mediator);

    assertEquals(0, p1.value);
    assertEquals(0, p2.value);

    p1.say(2);

    assertEquals(0, p1.value);
    assertEquals(2, p2.value);

    p2.say(4);

    assertEquals(4, p1.value);
    assertEquals(2, p2.value);

  }
}
