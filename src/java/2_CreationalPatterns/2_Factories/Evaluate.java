package com.activemesa.creational.factories.exercise;

import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class Evaluate
{
  @Test
  public void test()
  {
    PersonFactory pf = new PersonFactory();

    Person p1 = pf.createPerson("Chris");
    assertEquals("Chris", p1.name);
    assertEquals(0, p1.id);

    Person p2 = pf.createPerson("Sarah");
    assertEquals(1, p2.id);
  }
}
