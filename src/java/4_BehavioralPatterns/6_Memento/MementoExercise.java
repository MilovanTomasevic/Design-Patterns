package com.activemesa.behavioral.memento.exercise;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

class Token
{
  public int value = 0;

  public Token(int value)
  {
    this.value = value;
  }
}

class Memento
{
  public List<Token> tokens = new ArrayList<>();
}

class TokenMachine
{
  public List<Token> tokens = new ArrayList<>();

  public Memento addToken(int value)
  {
    return addToken(new Token(value));
  }

  public Memento addToken(Token token)
  {
    tokens.add(token);
    Memento m = new Memento();
    m.tokens = tokens.stream()
      .map(t -> new Token(t.value))
      .collect(Collectors.toList());
    return m;
  }

  public void revert(Memento m)
  {
    tokens = m.tokens.stream()
      .map(t -> new Token(t.value))
      .collect(Collectors.toList());
  }
}