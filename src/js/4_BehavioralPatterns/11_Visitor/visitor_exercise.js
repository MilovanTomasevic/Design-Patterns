class Integer
{
  constructor(value)
  {
    this.value = value;
  }

  accept(visitor)
  {
    visitor.visitValue(this);
  }
}

class BinaryExpression
{
  constructor(lhs, rhs)
  {
    this.lhs = lhs;
    this.rhs = rhs;
  }
}

class AdditionExpression extends BinaryExpression
{
  constructor(lhs, rhs) {
    super(lhs, rhs);
  }

  accept(visitor)
  {
    visitor.visitAddition(this);
  }
}

class MultiplicationExpression extends BinaryExpression
{
  constructor(lhs, rhs) {
    super(lhs, rhs);
  }

  accept(visitor)
  {
    visitor.visitMultiplication(this);
  }
}

class ExpressionPrinter
{
  constructor()
  {
    this._buffer = [];
  }

  visitValue(value)
  {
    this._buffer.push(value.value);
  }

  visitAddition(ae)
  {
    this._buffer.push('(');
    ae.lhs.accept(this);
    this._buffer.push('+');
    ae.rhs.accept(this);
    this._buffer.push(')');
  }

  visitMultiplication(me)
  {
    me.lhs.accept(this);
    this._buffer.push('*');
    me.rhs.accept(this);
  }

  toString()
  {
    return this._buffer.join('');
  }
}

describe('visitor', function()
{
  it('simple addition', function()
  {
    let simple = new AdditionExpression(
      new Integer(2), new Integer(3)
    );
    let ep = new ExpressionPrinter();
    ep.visitAddition(simple);
    expect(ep.toString()).toEqual('(2+3)');
  });

  it('product of addition and value', function()
  {
    const expr = new MultiplicationExpression(
      new AdditionExpression(new Integer(2), new Integer(3)),
      new Integer(4)
    );
    let ep = new ExpressionPrinter();
    ep.visitMultiplication(expr);
    expect(ep.toString()).toEqual('(2+3)*4');
  });
});