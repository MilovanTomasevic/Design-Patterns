package observer

import (
  "container/list"
  "fmt"
)

type Observable struct {
  subs *list.List
}

func (o *Observable) Subscribe(x Observer) {
  o.subs.PushBack(x)
}

func (o *Observable) Unsubscribe(x Observer) {
  for z := o.subs.Front(); z != nil; z = z.Next() {
    if z.Value.(Observer) == x {
      o.subs.Remove(z)
    }
  }
}

func (o *Observable) Fire(data interface{}) {
  for z := o.subs.Front(); z != nil; z = z.Next() {
    z.Value.(Observer).Notify(data)
  }
}

type Observer interface {
  Notify(data interface{})
}

type Person struct {
  Observable
  age int
}

func NewPerson(age int) *Person {
  return &Person{Observable{new(list.List)}, age}
}

type PropertyChanged struct {
  Name string
  Value interface{}
}

func (p *Person) Age() int { return p.age }
func (p *Person) SetAge(age int) {
  if age == p.age { return } // no change
  p.age = age
  p.Fire(PropertyChanged{"Age", p.age})
}

type TrafficManagement struct {
  o Observable
}

func (t *TrafficManagement) Notify(data interface{}) {
  if pc, ok := data.(PropertyChanged); ok {
    if pc.Value.(int) >= 16 {
      fmt.Println("Congrats, you can drive now!")
      // we no longer care
      t.o.Unsubscribe(t)
    }
  }
}

func main() {
  p := NewPerson(15)
  t := &TrafficManagement{p.Observable}
  p.Subscribe(t)

  for i := 16; i <= 20; i++ {
    fmt.Println("Setting age to", i)
    p.SetAge(i)
  }
}