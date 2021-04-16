using NUnit.Framework;

namespace DotNetDesignPatternDemos.Structural.Adapter
{
  namespace Coding.Exercise
  {
    public class Square
    {
      public int Side;
    }

    public interface IRectangle
    {
      int Width { get; }
      int Height { get; }
    }
    
    public static class ExtensionMethods
    {
      public static int Area(this IRectangle rc)
      {
        return rc.Width * rc.Height;
      }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
      private readonly Square square;

      public SquareToRectangleAdapter(Square square)
      {
        this.square = square;
      }

      public int Width => square.Side;
      public int Height => square.Side;
    }
  }

  namespace Coding.Exercise.Tests
  {
    [TestFixture]
    public class TestSuite
    {
      [Test]
      public void Test()
      {
        var sq = new Square{Side = 11};
        var adapter = new SquareToRectangleAdapter(sq);
        Assert.That(adapter.Area(), Is.EqualTo(121));
      }
    }
  }
}