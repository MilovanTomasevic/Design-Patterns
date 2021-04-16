package com.activemesa.behavioral.templatemethod.exercise;

class Creature
{
  public int attack, health;

  public Creature(int attack, int health)
  {
    this.attack = attack;
    this.health = health;
  }
}

abstract class CardGame
{
  public Creature [] creatures;

  public CardGame(Creature[] creatures)
  {
    this.creatures = creatures;
  }

  // return s-1 if no clear winner (both alive or both dead)
  public int combat(int creature1, int creature2)
  {
    Creature first = creatures[creature1];
    Creature second = creatures[creature2];
    hit(first, second);
    hit(second, first);
    boolean firstAlive = first.health > 0;
    boolean secondAlive = second.health > 0;
    if (firstAlive == secondAlive) return -1;
    return firstAlive ? creature1 : creature2;
  }

  // attacker hits other creature
  protected abstract void hit(Creature attacker, Creature other);
}

class TemporaryCardDamageGame extends CardGame
{
  public TemporaryCardDamageGame(Creature[] creatures)
  {
    super(creatures);
  }

  @Override
  protected void hit(Creature attacker, Creature other)
  {
    int oldHealth = other.health;
    other.health -= attacker.attack;
    if (other.health > 0)
      other.health = oldHealth;
  }
}

class PermanentCardDamageGame extends CardGame
{
  public PermanentCardDamageGame(Creature[] creatures)
  {
    super(creatures);
  }

  @Override
  protected void hit(Creature attacker, Creature other)
  {
    other.health -= attacker.attack;
  }
}