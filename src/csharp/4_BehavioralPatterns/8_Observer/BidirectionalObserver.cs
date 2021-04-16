using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using DotNetDesignPatternDemos.Annotations;

namespace DotNetDesignPatternDemos.Behavioral.Observer.Bidirectional
{
  public class Product : INotifyPropertyChanged
  {
    private string name;

    public string Name
    {
      get => name;
      set
      {
        if (value == name) return; // critical
        name = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged(
      [CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, 
        new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
      return $"Product: {Name}";
    }
  }

  public class Window : INotifyPropertyChanged
  {
    private string productName;

    public string ProductName
    {
      get => productName;
      set
      {
        if (value == productName) return; // critical
        productName = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public Window(Product product)
    {
      ProductName = product.Name;
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged(
      [CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, 
        new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
      return $"Window: {ProductName}";
    }
  }

  public sealed class BidirectionalBinding : IDisposable
  {
    private bool disposed;

    public BidirectionalBinding(
      INotifyPropertyChanged first,
      Expression<Func<object>> firstProperty,
      INotifyPropertyChanged second,
      Expression<Func<object>> secondProperty)
    {
      if (firstProperty.Body is MemberExpression firstExpr 
          && secondProperty.Body is MemberExpression secondExpr)
      {
        if (firstExpr.Member is PropertyInfo firstProp
            && secondExpr.Member is PropertyInfo secondProp)
        {
          first.PropertyChanged += (sender, args) =>
          {
            if (!disposed)
            {
              secondProp.SetValue(second, firstProp.GetValue(first));
            }
          };
          second.PropertyChanged += (sender, args) =>
          {
            if (!disposed)
            {
              firstProp.SetValue(first, secondProp.GetValue(second));
            }
          };
        }
      }
    }

    public void Dispose()
    {
      disposed = true;
    }
  }
  
  public class Program
  {
    public static void Main(string[] args)
    {
      var product = new Product{Name="Book"};
      var window = new Window(product);
      
      // want to ensure that when product name changes
      // in one component, it also changes in another

      // product.PropertyChanged += (sender, eventArgs) =>
      // {
      //   if (eventArgs.PropertyName == "Name")
      //   {
      //     Console.WriteLine("Name changed in Product");
      //     window.ProductName = product.Name;
      //   }
      // };
      //
      // window.PropertyChanged += (sender, eventArgs) =>
      // {
      //   if (eventArgs.PropertyName == "ProductName")
      //   {
      //     Console.WriteLine("Name changed in Window");
      //     product.Name = window.ProductName;
      //   }
      // };
      
      using var binding = new BidirectionalBinding(
        product,
        () => product.Name,
        window,
        () => window.ProductName);

      // there is no infinite loop because of
      // self-assignment guard
      product.Name = "Table";
      window.ProductName = "Chair";

      Console.WriteLine(product);
      Console.WriteLine(window);
    }
  }
}