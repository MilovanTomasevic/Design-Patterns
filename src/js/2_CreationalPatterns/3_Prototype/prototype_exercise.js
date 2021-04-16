class Point
{
  constructor(x, y)
  {
    this.x = x;
    this.y = y;
  }
}

class Line
{
  constructor(start, end)
  {
    this.start = start;
    this.end = end;
  }

  deepCopy()
  {
    let newStart = new Point(this.start.x, this.start.y);
    let newEnd = new Point(this.end.x, this.end.y);
    return new Line(newStart, newEnd);
  }
}

describe('prototype', function()
{
  it('test', function()
  {
    let line1 = new Line(
      new Point(3, 3),
      new Point(10, 10)
    );

    let line2 = line1.deepCopy();
    line1.start.x = line1.end.x =
      line1.end.x = line1.end.y = 0;

    expect(line2.start.x).toEqual(3);
    expect(line2.start.y).toEqual(3);
    expect(line2.end.x).toEqual(10);
    expect(line2.end.y).toEqual(10);
  });
});