package com.activemesa.behavioral.iterator.exercise;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

class Node<T>
{
  public T value;
  public Node<T> left, right, parent;

  public Node(T value)
  {
    this.value = value;
  }

  public Node(T value, Node<T> left, Node<T> right)
  {
    this.value = value;
    this.left = left;
    this.right = right;

    left.parent = right.parent = this;
  }

  private void traverse(Node<T> current,
                        ArrayList<Node<T>> acc)
  {
    acc.add(current);
    if (current.left != null)
    {
      traverse(current.left, acc);
    }
    if (current.right != null)
    {
      traverse(current.right, acc);
    }
  }

  public Iterator<Node<T>> preOrder()
  {
    ArrayList<Node<T>> items = new ArrayList<>();
    traverse(this, items);
    return items.iterator();
  }
}