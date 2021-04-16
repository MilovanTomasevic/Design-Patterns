package com.activemesa.behavioral.observer.exercise;

import java.io.Closeable;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.function.BiConsumer;

class Event<T>
{
  private List<BiConsumer<Object, T>> consumers = new ArrayList<>();

  public void subscribe(BiConsumer<Object, T> consumer)
  {
    consumers.add(consumer);
  }

  public void invoke(Object sender, T arg)
  {
    for (BiConsumer<Object, T> consumer : consumers)
      consumer.accept(sender, arg);
  }
}

class Game
{
  public Event<Void> ratEnters = new Event<>();
  public Event<Void> ratDies = new Event<>();
  public Event<Rat> notifyRat = new Event<>();
}

class Rat implements Closeable
{
  private Game game;
  public int attack = 1;

  public Rat(Game game)
  {
    this.game = game;
    game.ratEnters.subscribe((sender, arg) -> {
      if (sender != this)
      {
        ++attack;
        game.notifyRat.invoke(this, (Rat) sender);
      }
    });
    game.notifyRat.subscribe((sender, rat) -> {
      if (rat == this) ++attack;
    });
    game.ratDies.subscribe((sender, arg) -> --attack);
    game.ratEnters.invoke(this, null);
  }

  @Override
  public void close() throws IOException
  {
    // rat dies ;(
    game.ratDies.invoke(this, null);
  }
}