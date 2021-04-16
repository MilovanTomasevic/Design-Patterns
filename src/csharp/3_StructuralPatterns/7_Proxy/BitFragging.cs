using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DotNetDesignPatternDemos.Structural.Proxy.BitFragging
{

  public enum Op : byte
  {
    [Description("*")]
    Mul = 0,
    [Description("/")]
    Div = 1,
    [Description("+")]
    Add = 2,
    [Description("-")]
    Sub = 3
  }

  public static class OpImpl
  {
    static OpImpl()
    {
      var type = typeof(Op);
      foreach (Op op in Enum.GetValues(type))
      {
        MemberInfo[] memInfo = type.GetMember(op.ToString());
        if (memInfo.Length > 0)
        {
          var attrs = memInfo[0].GetCustomAttributes(
            typeof(DescriptionAttribute), false);

          if (attrs.Length > 0)
          {
            opNames[op] = ((DescriptionAttribute) attrs[0]).Description[0];
          }
        }
      }
    }

    private static readonly Dictionary<Op, char> opNames
      = new Dictionary<Op, char>();
    
    // notice the data types!
    private static readonly Dictionary<Op, Func<double, double, double>> opImpl =
      new Dictionary<Op, Func<double, double, double>>() {
        [Op.Mul] = ((x, y) => x * y),
        [Op.Div] = ((x, y) => x / y),
        [Op.Add] = ((x, y) => x + y),
        [Op.Sub] = ((x, y) => x - y),
      };

    public static double Call(this Op op, int x, int y)
    {
      return opImpl[op](x, y);
    }

    public static char Name(this Op op)
    {
      return opNames[op];
    }
  }
  
  public class Problem
  {
    private readonly List<int> numbers;
    private readonly List<Op> ops;

    public Problem(IEnumerable<int> numbers, IEnumerable<Op> ops)
    {
      this.numbers = new List<int>(numbers);
      this.ops = new List<Op>(ops);
    }

    public int Eval()
    {
      var opGroups = new[]
      {
        new[] {Op.Mul, Op.Div},
        new[] {Op.Add, Op.Sub}
      };
      startAgain:
      foreach (var group in opGroups)
      {
        for (var idx = 0; idx < ops.Count; ++idx)
        {
          if (group.Contains(ops[idx]))
          {
            // evaluate value
            var op = ops[idx];
            var result = op.Call(numbers[idx], numbers[idx + 1]);

            // assume all fractional results are wrong
            if (result != (int) result)
              return int.MinValue; // calculation won't work

            numbers[idx] = (int)result;
            numbers.RemoveAt(idx + 1);
            ops.RemoveAt(idx);
            if (numbers.Count == 1) return numbers[0];
            goto startAgain; // :)
          }
        }
      }

      return numbers[0];
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      int i = 0;
      
      for (; i < ops.Count; ++i)
      {
        sb.Append(numbers[i]);
        sb.Append(ops[i].Name());
      }

      sb.Append(numbers[i]);
      return sb.ToString();
    }
  }

  public class TwoBitSet
  {
    // 64 bits --> 32 values
    private readonly ulong data;

    public TwoBitSet(ulong data)
    {
      this.data = data;
    }

    public byte this[int index]
    {
      get
      {
        var shift = index << 1;
        ulong mask = (0b11U << shift);
        return (byte)((data & mask) >> shift);
      }
    }
  }

  class Program
  {
    static void Main()
    {
      var numbers = new[] {1, 3, 5, 7};
      int numberOfOps = numbers.Length - 1;
      
      for (int result = 0; result <= 10; ++result)
      {
        for (var key = 0UL; key < (1UL << 2*numberOfOps); ++key)
        {
          var tbs = new TwoBitSet(key);
          var ops = Enumerable.Range(0, numberOfOps)
            .Select(i => tbs[i]).Cast<Op>().ToArray();
          var problem = new Problem(numbers, ops);
          if (problem.Eval() == result)
          {
            Console.WriteLine($"{new Problem(numbers, ops)} = {result}");
            break;
          }
        }
      }

      Console.ReadKey();
    }
  }
}