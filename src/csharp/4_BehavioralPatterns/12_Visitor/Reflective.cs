using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;


namespace DotNetDesignPatternDemos.Behavioral.Visitor.Reflective
{
  using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;

  public abstract class Expression
  {
  }

  public class DoubleExpression : Expression
  {
    public double Value;

    public DoubleExpression(double value)
    {
      Value = value;
    }
  }

  public class AdditionExpression : Expression
  {
    public Expression Left;
    public Expression Right;

    public AdditionExpression(Expression left, Expression right)
    {
      Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
      Right = right ?? throw new ArgumentNullException(paramName: nameof(right));
    }
  }

  

  public static class ExpressionPrinter
  {
    private static DictType actions = new DictType
    {
      [typeof(DoubleExpression)] = (e, sb) =>
      {
        var de = (DoubleExpression) e;
        sb.Append(de.Value);
      },
      [typeof(AdditionExpression)] = (e, sb) =>
      {
        var ae = (AdditionExpression) e;
        sb.Append("(");
        Print(ae.Left, sb);
        sb.Append("+");
        Print(ae.Right, sb);
        sb.Append(")");
      }
    };

    public static void Print2(Expression e, StringBuilder sb)
    {
      actions[e.GetType()](e, sb);
    }

    public static void Print(Expression e, StringBuilder sb)
    {
      if (e is DoubleExpression de)
      {
        sb.Append(de.Value);
      }
      else 
      if (e is AdditionExpression ae)
      {
        sb.Append("(");
        Print(ae.Left, sb);
        sb.Append("+");
        Print(ae.Right, sb);
        sb.Append(")");
      }
      // breaks open-closed principle
      // will work incorrectly on missing case
    }
  }

  public class Demo
  {
    public static void Main()
    {
      var e = new AdditionExpression(
        left: new DoubleExpression(1),
        right: new AdditionExpression(
          left: new DoubleExpression(2),
          right: new DoubleExpression(3)));
      var sb = new StringBuilder();
      ExpressionPrinter.Print2(e, sb);
      WriteLine(sb);
    }
  }
}