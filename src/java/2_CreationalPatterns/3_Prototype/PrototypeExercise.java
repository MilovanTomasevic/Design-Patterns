package com.activemesa.creational.prototype.exercise;

class Point
{
  public int x, y;

  public Point(int x, int y)
  {
    this.x = x;
    this.y = y;
  }
}

class Line
{
  public Point start, end;

  public Line(Point start, Point end)
  {
    this.start = start;
    this.end = end;
  }

  public Line deepCopy()
  {
    Point newStart = new Point(start.x, start.y);
    Point newEnd = new Point(end.x, end.y);
    return new Line(newStart, newEnd);
  }
}

