package com.activemesa.structural.adapter.nocaching;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

class Point
{
  public int x, y;

  public Point(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  @Override
  public String toString()
  {
    return "Point{" +
      "first=" + x +
      ", second=" + y +
      '}';
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
}

class VectorObject extends ArrayList<Line> {}

class VectorRectangle extends VectorObject
{
  public VectorRectangle(int x, int y, int width, int height)
  {
    add(new Line(new Point(x,y), new Point(x+width, y) ));
    add(new Line(new Point(x+width,y), new Point(x+width, y+height) ));
    add(new Line(new Point(x,y), new Point(x, y+height) ));
    add(new Line(new Point(x,y+height), new Point(x+width, y+height) ));
  }
}

class LineToPointAdapter extends ArrayList<Point>
{
  private static int count = 0;

  public LineToPointAdapter(Line line)
  {
    System.out.println(
      String.format("%d: Generating points for line [%d,%d]-[%d,%d] (no caching)",
        ++count, line.start.x, line.start.y, line.end.x, line.end.y));

    int left = Math.min(line.start.x, line.end.x);
    int right = Math.max(line.start.x, line.end.x);
    int top = Math.min(line.start.y, line.end.y);
    int bottom = Math.max(line.start.y, line.end.y);
    int dx = right - left;
    int dy = line.end.y - line.start.y;

    if (dx == 0)
    {
      for (int y = top; y <= bottom; ++y)
      {
        add(new Point(left, y));
      }
    }
    else if (dy == 0)
    {
      for (int x = left; x <= right; ++x)
      {
        add(new Point(x, top));
      }
    }
  }
}

class AdapterDemo
{
  private static final List<VectorObject> vectorObjects =
    new ArrayList<>(Arrays.asList(
      new VectorRectangle(1,1,10,10),
      new VectorRectangle(3,3,6,6)
    ));

  public static void drawPoint(Point p)
  {
    System.out.print(".");
  }

  private static void draw()
  {
    for (VectorObject vo : vectorObjects)
    {
      for (Line line : vo)
      {
        LineToPointAdapter adapter = new LineToPointAdapter(line);
        adapter.forEach(AdapterDemo::drawPoint);
      }
    }
  }

  public static void main(String[] args)
  {
    draw();
    draw();
  }
}
