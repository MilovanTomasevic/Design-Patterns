class NumberExpression
{
  constructor(value)
  {
    this.value = value;
  }
}

class AdditionExpression
{
  constructor(left, right)
  {
    this.left = left;
    this.right = right;
  }
}

class ExpressionPrinter
{
  print(e, buffer)
  {
    if (e instanceof NumberExpression)
    {
      buffer.push(e.value);
    }
    else if (e instanceof AdditionExpression)
    {
      buffer.push('(');
      this.print(e.left, buffer);
      buffer.push('+');
      this.print(e.right, buffer);
      buffer.push(')');
    }
  }
}

let e = new AdditionExpression(
  new NumberExpression(1),
  new AdditionExpression(
    new NumberExpression(2),
    new NumberExpression(3)
  )
);
let buffer = [];
let ep = new ExpressionPrinter();
ep.print(e, buffer);
console.log(buffer.join(''));