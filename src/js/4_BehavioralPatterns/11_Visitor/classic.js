class NumberExpression
{
  constructor(value)
  {
    this.value = value;
  }

  accept(visitor)
  {
    visitor.visitNumber(this);
  }
}

class AdditionExpression
{
  constructor(left, right)
  {
    this.left = left;
    this.right = right;
  }

  accept(visitor)
  {
    visitor.visitAddition(this);
  }
}

class Visitor
{
  constructor()
  {
    this.buffer = [];
  }
}

class ExpressionPrinter extends Visitor
{
  constructor()
  {
    super();
  }

  visitNumber(e)
  {
    this.buffer.push(e.value);
  }

  visitAddition(e)
  {
    this.buffer.push('(');
    e.left.accept(this);
    this.buffer.push('+');
    e.right.accept(this);
    this.buffer.push(')');
  }

  toString()
  {
    return this.buffer.join('');
  }
}

class ExpressionCalculator
{
  // this visitor is stateful which can lead to problems
  constructor()
  {
    this.result = 0;
  }

  visitNumber(e)
  {
    this.result = e.value;
  }

  visitAddition(e)
  {
    e.left.accept(this);
    let temp = this.result;
    e.right.accept(this);
    this.result += temp;
  }
}

let e = new AdditionExpression(
  new NumberExpression(1),
  new AdditionExpression(
    new NumberExpression(2),
    new NumberExpression(3)
  )
);

var ep = new ExpressionPrinter();
ep.visitAddition(e);

var ec = new ExpressionCalculator();
ec.visitAddition(e);

console.log(
  `${ep.toString()} = ${ec.result}`
);
