package main

import (
  "bytes"
  "encoding/gob"
  "fmt"
)

type Address struct {
  Suite int
  StreetAddress, City string
}

type Employee struct {
  Name string
  Office Address
}

func (p *Employee) DeepCopy() *Employee {
  // note: no error handling below
  b := bytes.Buffer{}
  e := gob.NewEncoder(&b)
  _ = e.Encode(p)

  // peek into structure
  //fmt.Println(string(b.Bytes()))

  d := gob.NewDecoder(&b)
  result := Employee{}
  _ = d.Decode(&result)
  return &result
}

// employee factory
// either a struct or some functions
var mainOffice = Employee {
  "", Address{0, "123 East Dr", "London"}}
var auxOffice = Employee {
  "", Address{0, "66 West Dr", "London"}}

// utility method for configuring emp
//   ↓ lowercase
func newEmployee(proto *Employee,
  name string, suite int) *Employee {
  result := proto.DeepCopy()
  result.Name = name
  result.Office.Suite = suite
  return result
}

func NewMainOfficeEmployee(
  name string, suite int) *Employee {
    return newEmployee(&mainOffice, name, suite)
}

func NewAuxOfficeEmployee(
  name string, suite int) *Employee {
    return newEmployee(&auxOffice, name, suite)
}

func main() {
  // most people work in one of two offices

  //john := Employee{"John",
  //  Address{100, "123 East Dr", "London"}}
  //
  //jane := john.DeepCopy()
  //jane.Name = "Jane"
  //jane.Office.Suite = 200
  //jane.Office.StreetAddress = "66 West Dr"

  john := NewMainOfficeEmployee("John", 100)
  jane := NewAuxOfficeEmployee("Jane", 200)

  fmt.Println(john)
  fmt.Println(jane)
}


