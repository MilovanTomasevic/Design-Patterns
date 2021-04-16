package state

import (
  "bufio"
  "fmt"
  "os"
  "strconv"
)

type State int

const (
  OffHook State = iota
  Connecting
  Connected
  OnHold
  OnHook
)

func (s State) String() string {
  switch s {
  case OffHook: return "OffHook"
  case Connecting: return "Connecting"
  case Connected: return "Connected"
  case OnHold: return "OnHold"
  case OnHook: return "OnHook"
  }
  return "Unknown"
}

type Trigger int

const (
  CallDialed Trigger = iota
  HungUp
  CallConnected
  PlacedOnHold
  TakenOffHold
  LeftMessage
)

func (t Trigger) String() string {
  switch t {
  case CallDialed: return "CallDialed"
  case HungUp: return "HungUp"
  case CallConnected: return "CallConnected"
  case PlacedOnHold: return "PlacedOnHold"
  case TakenOffHold: return "TakenOffHold"
  case LeftMessage: return "LeftMessage"
  }
  return "Unknown"
}

type TriggerResult struct {
  Trigger Trigger
  State State
}

var rules = map[State][]TriggerResult {
  OffHook: {
    {CallDialed, Connecting},
  },
  Connecting: {
    {HungUp, OffHook},
    {CallConnected, Connected},
  },
  Connected: {
    {LeftMessage, OnHook},
    {HungUp, OnHook},
    {PlacedOnHold, OnHold},
  },
  OnHold: {
    {TakenOffHold, Connected},
    {HungUp, OnHook},
  },
}

func main() {
  state, exitState := OffHook, OnHook
  for ok := true; ok; ok = state != exitState {
    fmt.Println("The phone is currently", state)
    fmt.Println("Select a trigger:")

    for i := 0; i < len(rules[state]); i++ {
      tr := rules[state][i]
      fmt.Println(strconv.Itoa(i), ".", tr.Trigger)
    }

    input, _, _ := bufio.NewReader(os.Stdin).ReadLine()
    i, _ := strconv.Atoi(string(input))

    tr := rules[state][i]
    state = tr.State
  }
  fmt.Println("We are done using the phone")
}