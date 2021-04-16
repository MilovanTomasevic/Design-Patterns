using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows;
using ImpromptuInterface;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using static System.Console;

namespace DesignPatterns
{
  public class Creature : IEnumerable<int>
  {
    private int [] stats = new int[3];

    private const int strength = 0;

    public int Strength
    {
      get => stats[strength];
      set => stats[strength] = value;
    }

    public int Agility { get; set; }
    public int Intelligence { get; set; }

    public double AverageStat => 
      stats.Average();

    public IEnumerator<int> GetEnumerator()
    {
      return stats.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public int this[int index]
    {
      get { return stats[index]; }
      set { stats[index] = value; }
    }
  }

  public class Demo
  {
    static void Main(string[] args)
    {
    }
  }
}