package com.activemesa.structural.adapter.exercise;

import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class Evaluate
{
  @Test
  public void test()
  {
    Square sq = new Square(11);
    SquareToRectangleAdapter adapter = new SquareToRectangleAdapter(sq);
    assertEquals(121, adapter.getArea());
  }
}
