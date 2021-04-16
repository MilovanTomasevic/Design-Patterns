using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace DotNetDesignPatternDemos.Structural.Iterator.TreeTraversal
{
  public class Node<T>
  {
    public T Value;
    public Node<T> Left, Right;
    public Node<T> Parent;

    public Node(T value)
    {
      this.Value = value;
    }

    public Node(T value, Node<T> left, Node<T> right)
    {
      this.Value = value;
      this.Left = left;
      this.Right = right;

      left.Parent = right.Parent = this;
    }
  }

  public class InOrderIterator<T>
  {
    public Node<T> Current { get; set; }
    private readonly Node<T> root;
    private bool yieldedStart;

    public InOrderIterator(Node<T> root)
    {
      this.root = Current = root;
      while (Current.Left != null)
        Current = Current.Left;
    }

    

    public void Reset()
    {
      Current = root;
      yieldedStart = true;
    }

    public bool MoveNext()
    {
      if (!yieldedStart)
      {
        yieldedStart = true;
        return true;
      }

      if (Current.Right != null)
      {
        Current = Current.Right;
        while (Current.Left != null)
          Current = Current.Left;
        return true;
      }
      else
      {
        var p = Current.Parent;
        while (p != null && Current == p.Right)
        {
          Current = p;
          p = p.Parent;
        }
        Current = p;
        return Current != null;
      }
    }
  }


  public class BinaryTree<T>
  {
    private Node<T> root;

    public BinaryTree(Node<T> root)
    {
      this.root = root;
    }

    public InOrderIterator<T> GetEnumerator()
    {
      return new InOrderIterator<T>(root);
    }

    public IEnumerable<Node<T>> NaturalInOrder
    {
      get
      {
        IEnumerable<Node<T>> TraverseInOrder(Node<T> current)
        {
          if (current.Left != null)
          {
            foreach (var left in TraverseInOrder(current.Left))
              yield return left;
          }
          yield return current;
          if (current.Right != null)
          {
            foreach (var right in TraverseInOrder(current.Right))
              yield return right;
          }
        }
        foreach (var node in TraverseInOrder(root))
          yield return node;
      }
    }
  }

  public class Demo
  {
    public static void Main()
    {
      //   1
      //  / \
      // 2   3

      // in-order:  213
      // preorder:  123
      // postorder: 231

      var root = new Node<int>(1,
        new Node<int>(2), new Node<int>(3));

      // C++ style
      var it = new InOrderIterator<int>(root);

      while (it.MoveNext())
      {
        Write(it.Current.Value);
        Write(',');
      }
      WriteLine();

      // C# style
      var tree = new BinaryTree<int>(root);

      WriteLine(string.Join(",", tree.NaturalInOrder.Select(x => x.Value)));

      // duck typing!
      foreach (var node in tree)
        WriteLine(node.Value);
    }
  }
}