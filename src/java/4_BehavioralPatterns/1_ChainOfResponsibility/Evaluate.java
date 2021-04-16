package com.activemesa.behavioral.chainofresponsibility.exercise;

import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class Evaluate
{
  @Test
  public void manyGoblinsTest()
  {
    Game game = new Game();
    Goblin goblin = new Goblin(game);
    game.creatures.add(goblin);

    assertEquals(1, goblin.getAttack());
    assertEquals(1, goblin.getDefense());

    Goblin goblin2 = new Goblin(game);
    game.creatures.add(goblin2);

    assertEquals(1, goblin.getAttack());
    assertEquals(2, goblin.getDefense());

    GoblinKing goblin3 = new GoblinKing(game);
    game.creatures.add(goblin3);

    assertEquals(2, goblin.getAttack());
    assertEquals(3, goblin.getDefense());
  }
}
