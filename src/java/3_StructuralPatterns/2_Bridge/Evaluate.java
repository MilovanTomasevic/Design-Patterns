package com.activemesa.structural.bridge.exercise;

import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class Evaluate
{
  @Test
  public void test()
  {
    assertEquals("Drawing Square as lines",
      new Square(new VectorRenderer()).toString());
  }
}
