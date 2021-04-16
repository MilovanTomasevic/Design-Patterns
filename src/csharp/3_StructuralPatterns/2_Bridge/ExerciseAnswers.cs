using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DotNetDesignPatternDemos.Structural.Bridge
{
  namespace Coding.Exercise
  {
    //public abstract class Shape
    //{
    //  public string Name { get; set; }
    //}

    //public class Triangle : Shape
    //{
    //  public Triangle() => Name = "Circle";
    //}

    //public class Square : Shape
    //{
    //  public Square() => Name = "Square";
    //}

    //public class VectorSquare
    //{
    //  public override string ToString() => "Drawing {Name} as lines";
    //}

    //public class RasterSquare
    //{
    //  public override string ToString() => "Drawing {Name} as pixels";
    //}

    //// imagine VectorTriangle and RasterTriangle are here too

    public interface IRenderer
    {
      string WhatToRenderAs { get; }
    }

    public abstract class Shape
    {
      private IRenderer rendering;

      protected Shape(IRenderer rendering)
      {
        this.rendering = rendering;
      }

      public string Name { get; set; }

      public override string ToString()
      {
        return $"Drawing {Name} as {rendering.WhatToRenderAs}";
      }
    }

    public class Triangle : Shape
    {
      public Triangle(IRenderer strategy) : base(strategy)
      {
        Name = "Triangle";
      }
    }

    public class Square : Shape
    {
      public Square(IRenderer rendering) : base(rendering)
      {
        Name = "Square";
      }
    }

    public class RasterRenderer : IRenderer
    {
      public string WhatToRenderAs
      {
        get { return "pixels"; }
      }
    }

    public class VectorRenderer : IRenderer
    {
      public string WhatToRenderAs
      {
        get { return "lines"; }
      }
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
        Assert.That(
          new Square(new VectorRenderer()).ToString(),
          Is.EqualTo("Drawing Square as lines"));
      }
    }
  }
}