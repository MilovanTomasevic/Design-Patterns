package com.activemesa.behavioral.observer.exercise;

import org.junit.Test;

import java.io.IOException;

import static org.junit.Assert.assertEquals;

public class Evaluate
{
  @Test
  public void singleRatTest()
  {
    Game game = new Game();
    Rat rat = new Rat(game);
    assertEquals(1, rat.attack);
  }

  @Test
  public void twoRatTest()
  {
    Game game = new Game();
    Rat rat = new Rat(game);
    Rat rat2 = new Rat(game);
    assertEquals(2, rat.attack);
    assertEquals(2, rat2.attack);
  }

  @Test
  public void threeRatsOneDies()
    throws IOException
  {
    Game game = new Game();

    Rat rat = new Rat(game);
    assertEquals(1, rat.attack);

    Rat rat2 = new Rat(game);
    assertEquals(2, rat.attack);
    assertEquals(2, rat2.attack);

    try (Rat rat3 = new Rat(game))
    {
      assertEquals(3, rat.attack);
      assertEquals(3, rat2.attack);
      assertEquals(3, rat3.attack);
    }

    assertEquals(2, rat.attack);
    assertEquals(2, rat2.attack);
  }
}
