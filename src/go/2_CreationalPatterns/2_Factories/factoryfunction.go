package factories

import "fmt"

type Person struct {
  Name string
  Age int
}

// factory function
//func NewPerson(name string, age int) Person {
//  return Person{name, age}
//}

func NewPerson(name string, age int) *Person {
  return &Person{name, age}
}

func main_() {
  // initialize directly
  p := Person{"John", 22}
  fmt.Println(p)

  // use a constructor
  p2 := NewPerson("Jane", 21)
  p2.Age = 30
  fmt.Println(p2)
}
