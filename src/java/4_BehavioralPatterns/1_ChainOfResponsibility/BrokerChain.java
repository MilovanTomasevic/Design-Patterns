package com.activemesa.behavioral.chainofresponsibility.broker;

import java.util.HashMap;
import java.util.Map;
import java.util.function.*;

class Event<TArgs>
{
//  private List<Consumer<TArgs>> handlers
//    = new ArrayList<>();
  private int index = 0;
  private Map<Integer, Consumer<TArgs>>
    handlers = new HashMap<>();

  public int subscribe(Consumer<TArgs> handler)
  {
    //handlers.add(handler);
    int i = index;
    handlers.put(index++, handler);
    return i;
  }

  public void unsubscribe(int key)
  {
    handlers.remove(key);
  }

  public void fire(TArgs args)
  {
    for (Consumer<TArgs> handler : handlers.values())
      handler.accept(args);
  }
}

class Query
{
  public String creatureName;
  enum Argument
  {
    ATTACK, DEFENSE
  }
  public Argument argument;
  public int result;

  public Query(String creatureName, Argument argument, int result)
  {
    this.creatureName = creatureName;
    this.argument = argument;
    this.result = result;
  }
}

class Game
{
  public Event<Query> queries = new Event<>();
}

class Creature
{
  private Game game;
  public String name;
  private int baseAttack, baseDefense;

  public Creature(Game game, String name,
                  int baseAttack, int baseDefense)
  {
    this.game = game;
    this.baseAttack = baseAttack;
    this.baseDefense = baseDefense;
    this.name = name;
  }

  int getAttack() {
    Query q = new Query(name, Query.Argument.ATTACK, baseAttack);
    game.queries.fire(q);
    return q.result;
  }

  int getDefense() {
    Query q = new Query(name, Query.Argument.DEFENSE, baseDefense);
    game.queries.fire(q);
    return q.result;
  }

  @Override
  public String toString() // avoid printing Game
  {
    return "Creature{" +
      "name='" + name + '\'' +
      ", attack=" + getAttack() + // !!!
      ", defense=" + getDefense() +
      '}';
  }
}

class CreatureModifier
{ // protected, not private!
  protected Game game;
  protected Creature creature;

  public CreatureModifier(Game game, Creature creature)
  {
    this.game = game;
    this.creature = creature;
  }
}

class IncreasedDefenseModifier
  extends CreatureModifier
{

  public IncreasedDefenseModifier(Game game, Creature creature)
  {
    super(game, creature);

    game.queries.subscribe(q -> {
      if (q.creatureName.equals(creature.name)
        && q.argument == Query.Argument.DEFENSE)
      {
        q.result += 3;
      }
    });
  }
}

class DoubleAttackModifier
  extends CreatureModifier
  implements AutoCloseable
{
  private int token;
  public DoubleAttackModifier(Game game, Creature creature)
  {
    super(game, creature);

    token = game.queries.subscribe(q -> {
      if (q.creatureName.equals(creature.name)
        && q.argument == Query.Argument.ATTACK)
      {
        q.result *= 2;
      }
    });
  }

  @Override // commented out exception
  public void close() /*throws Exception*/
  {
    game.queries.unsubscribe(token);
  }
}

class BrokerChainDemo
{
  public static void main(String[] args)
  {
    Game game = new Game();
    Creature goblin = new Creature(game,
      "Strong Goblin", 2, 2);

    System.out.println(goblin);

    // modifiers can be piled up
    IncreasedDefenseModifier icm = new IncreasedDefenseModifier(game, goblin);

    try (DoubleAttackModifier dam
      = new DoubleAttackModifier(game, goblin))
    {
      System.out.println(goblin);
    }

    System.out.println(goblin);
  }
}