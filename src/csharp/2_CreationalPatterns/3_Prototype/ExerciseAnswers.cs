using NUnit.Framework;

namespace DotNetDesignPatternDemos.Creational.Prototype
{
  namespace Coding.Exercise
  {
    public class Point
    {
      public int X, Y;
    }

    public class Line
    {
      public Point Start, End;

      public Line DeepCopy()
      {
        var newStart = new Point{X = Start.X, Y = Start.Y};
        var newEnd = new Point {X = End.X, Y = End.Y};
        return new Line {Start = newStart, End = newEnd};
      }
    }
  }

  namespace Coding.Exercise.UnitTests
  {
    [TestFixture]
    public class FirstTestSuite
    {
      [Test]
      public void Test()
      {
        var line1 = new Line
        {
          Start = new Point { X = 3, Y = 3},
          End = new Point { X = 10, Y = 10}
        };

        var line2 = line1.DeepCopy();
        line1.Start.X = line1.End.X = line1.Start.Y = line1.End.Y = 0;

        Assert.That(line2.Start.X, Is.EqualTo(3));
        Assert.That(line2.Start.Y, Is.EqualTo(3));
        Assert.That(line2.End.X, Is.EqualTo(10));
        Assert.That(line2.End.Y, Is.EqualTo(10));
      }
    }
  }
}