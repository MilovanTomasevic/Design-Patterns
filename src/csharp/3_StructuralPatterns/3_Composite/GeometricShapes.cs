using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DotNetDesignPatternDemos.Structural.Composite.GeometricShapes
{
  public class GraphicObject
  {
    public virtual string Name { get; set; } = "Group";
    public string Color;
    private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
    public List<GraphicObject> Children => children.Value;

    private void Print(StringBuilder sb, int depth)
    {
      sb.Append(new string('*', depth))
        .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
        .AppendLine($"{Name}");
      foreach (var child in Children)
        child.Print(sb, depth + 1);
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      Print(sb, 0);
      return sb.ToString();
    }
  }
  
  public class Circle : GraphicObject
  {
    public override string Name => "Circle";
  }

  public class Square : GraphicObject
  {
    public override string Name => "Square";
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      var drawing = new GraphicObject {Name = "My Drawing"};
      drawing.Children.Add(new Square {Color = "Red"});
      drawing.Children.Add(new Circle{Color="Yellow"});
      
      var group = new GraphicObject();
      group.Children.Add(new Circle{Color="Blue"});
      group.Children.Add(new Square{Color="Blue"});
      drawing.Children.Add(group);

      WriteLine(drawing);
    }
  }
}
