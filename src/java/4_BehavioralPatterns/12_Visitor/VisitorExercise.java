package com.activemesa.behavioral.visitor.exercise;

abstract class ExpressionVisitor
{
  abstract void visit(Value value);
  abstract void visit(AdditionExpression ae);
  abstract void visit(MultiplicationExpression me);
}

abstract class Expression
{
  abstract void accept(ExpressionVisitor ev);
}

class Value extends Expression
{
  public int value;

  public Value(int value)
  {
    this.value = value;
  }

  @Override
  void accept(ExpressionVisitor ev)
  {
    ev.visit(this);
  }
}

class AdditionExpression extends Expression
{
  public Expression lhs, rhs;

  public AdditionExpression(Expression lhs, Expression rhs)
  {
    this.lhs = lhs;
    this.rhs = rhs;
  }

  @Override
  void accept(ExpressionVisitor ev)
  {
    ev.visit(this);
  }
}

class MultiplicationExpression extends Expression
{
  public Expression lhs, rhs;

  public MultiplicationExpression(Expression lhs, Expression rhs)
  {
    this.lhs = lhs;
    this.rhs = rhs;
  }

  @Override
  void accept(ExpressionVisitor ev)
  {
    ev.visit(this);
  }
}

class ExpressionPrinter extends ExpressionVisitor
{
  private StringBuilder sb = new StringBuilder();

  @Override
  void visit(Value value)
  {
    sb.append(value.value);
  }
  @Override
  void visit(AdditionExpression ae)
  {
    sb.append("(");
    ae.lhs.accept(this);
    sb.append("+");
    ae.rhs.accept(this);
    sb.append(")");
  }
  @Override
  void visit(MultiplicationExpression me)
  {
    me.lhs.accept(this);
    sb.append("*");
    me.rhs.accept(this);
  }

  @Override
  public String toString()
  {
    return sb.toString();
  }
}