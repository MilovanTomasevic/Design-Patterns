package com.activemesa.behavioral.mediator.exercise;

import java.util.ArrayList;
import java.util.List;

class Participant
{
  private Mediator mediator;
  public int value;

  public Participant(Mediator mediator)
  {
    this.mediator = mediator;
    mediator.addListener(this);
  }

  public void say(int n)
  {
    mediator.broadcast(this, n);
  }

  public void notify(Object sender, int n)
  {
    if (sender != this)
      value += n;
  }
}

class Mediator
{
  private List<Participant> listeners = new ArrayList<>();

  public void broadcast(Object sender, int n)
  {
    for (Participant p : listeners)
      p.notify(sender, n);
  }

  public void addListener(Participant p)
  {
    listeners.add(p);
  }
}