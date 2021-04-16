using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Console;

namespace DesignPatterns
{
  public enum State
  {
    OffHook,
    Connecting,
    Connected,
    OnHold
  }

  public enum Trigger
  {
    CallDialed,
    HungUp,
    CallConnected,
    PlacedOnHold,
    TakenOffHold,
    LeftMessage
  }

  class Demo
  {
    private static Dictionary<State, List<(Trigger, State)>> rules
      = new Dictionary<State, List<(Trigger, State)>>
      {
        [State.OffHook] = new List<(Trigger, State)>
        {
          (Trigger.CallDialed, State.Connecting)
        },
        [State.Connecting] = new List<(Trigger, State)>
        {
          (Trigger.HungUp, State.OffHook),
          (Trigger.CallConnected, State.Connected)
        },
        [State.Connected] = new List<(Trigger, State)>
        {
          (Trigger.LeftMessage, State.OffHook),
          (Trigger.HungUp, State.OffHook),
          (Trigger.PlacedOnHold, State.OnHold)
        },
        [State.OnHold] = new List<(Trigger, State)>
        {
          (Trigger.TakenOffHold, State.Connected),
          (Trigger.HungUp, State.OffHook)
        }
      };

    static void Main(string[] args)
    {
      var state = State.OffHook;
      while (true)
      {
        WriteLine($"The phone is currently {state}");
        WriteLine("Select a trigger:");

        // foreach to for
        for (var i = 0; i < rules[state].Count; i++)
        {
          var (t, _) = rules[state][i];
          WriteLine($"{i}. {t}");
        }


        int input = int.Parse(Console.ReadLine());

        var (_, s) = rules[state][input];
        state = s;
      }
    }
  }
}