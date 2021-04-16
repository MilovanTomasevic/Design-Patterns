using System;
using System.Collections.Generic;
using static System.Console;

namespace DotNetDesignPatternDemos.SOLID.OCP
{
  public enum Color
  {
    Red, Green, Blue
  }

  public enum Size
  {
    Small, Medium, Large, Yuge
  }

  public class Product
  {
    public string Name;
    public Color Color;
    public Size Size;

    public Product(string name, Color color, Size size)
    {
      Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
      Color = color;
      Size = size;
    }
  }

  public class ProductFilter
  {
    // let's suppose we don't want ad-hoc queries on products
    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
      foreach (var p in products)
        if (p.Color == color)
          yield return p;
    }
    
    public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
      foreach (var p in products)
        if (p.Size == size)
          yield return p;
    }

    public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
    {
      foreach (var p in products)
        if (p.Size == size && p.Color == color)
          yield return p;
    } // state space explosion
      // 3 criteria = 7 methods

    // OCP = open for extension but closed for modification
  }

  // we introduce two new interfaces that are open for extension

  public interface ISpecification<T>
  {
    bool IsSatisfied(Product p);
  }

  public interface IFilter<T>
  {
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
  }

  public class ColorSpecification : ISpecification<Product>
  {
    private Color color;

    public ColorSpecification(Color color)
    {
      this.color = color;
    }

    public bool IsSatisfied(Product p)
    {
      return p.Color == color;
    }
  }

  public class SizeSpecification : ISpecification<Product>
  {
    private Size size;

    public SizeSpecification(Size size)
    {
      this.size = size;
    }

    public bool IsSatisfied(Product p)
    {
      return p.Size == size;
    }
  }

  // combinator
  public class AndSpecification<T> : ISpecification<T>
  {
    private ISpecification<T> first, second;

    public AndSpecification(ISpecification<T> first, ISpecification<T> second)
    {
      this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
      this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
    }

    public bool IsSatisfied(Product p)
    {
      return first.IsSatisfied(p) && second.IsSatisfied(p);
    }
  }

  public class BetterFilter : IFilter<Product>
  {
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
    {
      foreach (var i in items)
        if (spec.IsSatisfied(i))
          yield return i;
    }
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      var apple = new Product("Apple", Color.Green, Size.Small);
      var tree = new Product("Tree", Color.Green, Size.Large);
      var house = new Product("House", Color.Blue, Size.Large);

      Product[] products = {apple, tree, house};

      var pf = new ProductFilter();
      WriteLine("Green products (old):");
      foreach (var p in pf.FilterByColor(products, Color.Green))
        WriteLine($" - {p.Name} is green");

      // ^^ BEFORE

      // vv AFTER
      var bf = new BetterFilter();
      WriteLine("Green products (new):");
      foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
        WriteLine($" - {p.Name} is green");

      WriteLine("Large products");
      foreach (var p in bf.Filter(products, new SizeSpecification(Size.Large)))
        WriteLine($" - {p.Name} is large");

      WriteLine("Large blue items");
      foreach (var p in bf.Filter(products,
        new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large)))
      )
      {
        WriteLine($" - {p.Name} is big and blue");
      }
    }
  }
}
