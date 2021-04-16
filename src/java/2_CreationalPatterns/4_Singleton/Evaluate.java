package com.activemesa.creational.singleton.exercise;

import org.junit.Test;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.assertFalse;

public class Evaluate
{
  @Test
  public void test()
  {
    Object obj = new Object();
    assertTrue(SingletonTester.isSingleton(() -> obj));
    assertFalse(SingletonTester.isSingleton(Object::new));
  }
}
