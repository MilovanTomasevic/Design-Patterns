package prototype

import "fmt"

type Address struct {
  StreetAddress, City, Country string
}

type Person struct {
  Name string
  Address *Address
}

func main() {
  john := Person{"John",
    &Address{"123 London Rd", "London", "UK"}}

  //jane := john

  // shallow copy
  //jane.Name = "Jane" // ok

  //jane.Address.StreetAddress = "321 Baker St"

  //fmt.Println(john.Name, john.Address)
  //fmt.Println(jane.Name, jane. Address)

  // what you really want
  jane := john
  jane.Address = &Address{
    john.Address.StreetAddress,
    john.Address.City,
    john.Address.Country  }

  jane.Name = "Jane" // ok

  jane.Address.StreetAddress = "321 Baker St"

  fmt.Println(john.Name, john.Address)
  fmt.Println(jane.Name, jane. Address)
}
