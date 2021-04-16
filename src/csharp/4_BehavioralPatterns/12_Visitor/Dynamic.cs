using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DotNetDesignPatternDemos.Behavioral.Visitor.Dynamic
{
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

  public class ExpressionPrinter
  {
    public void Print(AdditionExpression ae, StringBuilder sb)
    {
      sb.Append("(");
      Print((dynamic)ae.Left, sb);
      sb.Append("+");
      Print((dynamic)ae.Right, sb);
      sb.Append(")");
    }

    public void Print(DoubleExpression de, StringBuilder sb)
    {
      sb.Append(de.Value);
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
      var ep = new ExpressionPrinter();
      var sb = new StringBuilder();
      ep.Print((dynamic)e, sb);
      WriteLine(sb);

      // disadvantages:

      // 1) Performance penalty
      // 2) Runtime error on missing visitor
      // 3) Problematic w.r.t. inheritance
    }
  }
}