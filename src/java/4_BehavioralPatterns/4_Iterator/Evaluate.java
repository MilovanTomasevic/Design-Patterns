package com.activemesa.behavioral.iterator.exercise;

import org.junit.Test;

import java.util.Iterator;

import static org.junit.Assert.assertEquals;

public class Evaluate
{
  @Test
  public void test()
  {
    Node<Character> node = new Node<>('a',
      new Node<>('b',
        new Node<>('c'),
        new Node<>('d')),
      new Node<>('e'));
    StringBuilder sb = new StringBuilder();
    Iterator<Node<Character>> it = node.preOrder();
    while (it.hasNext())
    {
      sb.append(it.next().value);
    }
    assertEquals("abcde", sb.toString());
  }
}
