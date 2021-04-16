using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetDesignPatternDemos.Creational.Singleton.AmbientStack
{
  // non-thread-safe global context
  public sealed class BuildingContext : IDisposable
  {
    public int WallHeight = 0;
    public int WallThickness = 300; // etc.
    private static Stack<BuildingContext> stack 
      = new Stack<BuildingContext>();

    static BuildingContext()
    {
      // ensure there's at least one state
      stack.Push(new BuildingContext(0));
    }

    public BuildingContext(int wallHeight)
    {
      WallHeight = wallHeight;
      stack.Push(this);
    }

    public static BuildingContext Current => stack.Peek();

    public void Dispose()
    {
      // not strictly necessary
      if (stack.Count > 1)
        stack.Pop();
    }
  }

  public class Building
  {
    public readonly List<Wall> Walls = new List<Wall>();

    public override string ToString()
    {
      var sb = new StringBuilder();
      foreach (var wall in Walls)
        sb.AppendLine(wall.ToString());
      return sb.ToString();
    }
  }
  
  public struct Point
  {
    private int X, Y;

    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }

    public override string ToString()
    {
      return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
    }
  }

  public class Wall
  {
    public Point Start, End;
    public int Height;
    
    public const int UseAmbient = Int32.MinValue;

    // public Wall(Point start, Point end, int elevation = UseAmbient)
    // {
    //   Start = start;
    //   End = end;
    //   Elevation = elevation;
    // }

    public Wall(Point start, Point end)
    {
      Start = start;
      End = end;
      //Elevation = BuildingContext.Elevation;
      Height = BuildingContext.Current.WallHeight;
    }

    public override string ToString()
    {
      return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, " +
             $"{nameof(Height)}: {Height}";
    }
  }
  
  public class AmbientContextDemo
  {
    public static void Main(string[] args)
    {
      var house = new Building();
      
      // ground floor
      //var e = 0;
      house.Walls.Add(new Wall(new Point(0,0), new Point(5000,0)/*, e*/));
      house.Walls.Add(new Wall(new Point(0,0), new Point(0,4000)/*, e*/));
      
      // first floor
      //e = 3500;
      using (new BuildingContext(3500))
      {
        house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0) /*, e*/));
        house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000) /*, e*/));
      }

      // back to ground again
      // e = 0;
      house.Walls.Add(new Wall(new Point(5000,0), new Point(5000, 4000)/*, e*/));

      Console.WriteLine(house);
    }
  }
}