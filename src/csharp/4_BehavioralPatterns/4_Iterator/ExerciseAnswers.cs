using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Behavioral.Iterator
{
  namespace Coding.Exercise
  {
    public class Node<T>
    {
      public T Value;
      public Node<T> Left, Right;
      public Node<T> Parent;

      public Node(T value)
      {
        Value = value;
      }

      public Node(T value, Node<T> left, Node<T> right)
      {
        Value = value;
        Left = left;
        Right = right;

        left.Parent = right.Parent = this;
      }

      private IEnumerable<Node<T>> Traverse(Node<T> current)
      {
        yield return current;
        if (current.Left != null)
        {
          foreach (var left in Traverse(current.Left))
            yield return left;
        }
        if (current.Right != null)
        {
          foreach (var right in Traverse(current.Right))
            yield return right;
        }
      }

      public IEnumerable<T> PreOrder
      {
        get
        {
          foreach (var node in Traverse(this))
            yield return node.Value;
        }
      }
    }
  }

  namespace Coding.Exercise.Tests
  {
    [TestFixture]
    public class TestSuite
    {
      [Test]
      public void Test()
      {
        var node = new Node<char>('a',
          new Node<char>('b',
            new Node<char>('c'),
            new Node<char>('d')),
          new Node<char>('e'));
        Assert.That(new string(node.PreOrder.ToArray()), Is.EqualTo("abcde"));
      }
    }
  }
}