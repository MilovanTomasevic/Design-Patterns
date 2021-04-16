class Square
{
  constructor(side)
  {
    this.side = side;
  }
}

function area(rectangle)
{
  return rectangle.width * rectangle.height;
}

class SquareToRectangleAdapter
{
  constructor(square)
  {
    this.square = square;
  }

  get width() {
    return this.square.side;
  }

  get height() {
    return this.square.side;
  }
}

describe('adapter', function()
{
  it('should adapt things, duh!', function()
  {
    let sq = new Square(11);
    let adapter = new SquareToRectangleAdapter(sq);
    expect(area(adapter)).toEqual(121);
  });
});