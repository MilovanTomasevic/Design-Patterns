package main

import "fmt"

type Person struct {
  StreetAddress, Postcode, City string
  CompanyName, Position string
  AnnualIncome int
}

type PersonBuilder struct {
  person *Person // needs to be inited
}

func NewPersonBuilder() *PersonBuilder {
  return &PersonBuilder{&Person{}}
}

func (it *PersonBuilder) Build() *Person {
  return it.person
}

func (it *PersonBuilder) Works() *PersonJobBuilder {
  return &PersonJobBuilder{*it}
}

func (it *PersonBuilder) Lives() *PersonAddressBuilder {
  return &PersonAddressBuilder{*it}
}

type PersonJobBuilder struct {
  PersonBuilder
}

func (pjb *PersonJobBuilder) At(
  companyName string) *PersonJobBuilder {
  pjb.person.CompanyName = companyName
  return pjb
}

func (pjb *PersonJobBuilder) AsA(
  position string) *PersonJobBuilder {
  pjb.person.Position = position
  return pjb
}

func (pjb *PersonJobBuilder) Earning(
  annualIncome int) *PersonJobBuilder {
  pjb.person.AnnualIncome = annualIncome
  return pjb
}

type PersonAddressBuilder struct {
  PersonBuilder
}

func (it *PersonAddressBuilder) At(
  streetAddress string) *PersonAddressBuilder {
  it.person.StreetAddress = streetAddress
  return it
}

func (it *PersonAddressBuilder) In(
  city string) *PersonAddressBuilder {
  it.person.City = city
  return it
}

func (it *PersonAddressBuilder) WithPostcode(
  postcode string) *PersonAddressBuilder {
  it.person.Postcode = postcode
  return it
}

func main() {
  pb := NewPersonBuilder()
  pb.
    Lives().
      At("123 London Road").
      In("London").
      WithPostcode("SW12BC").
    Works().
      At("Fabrikam").
      AsA("Programmer").
      Earning(123000)
  person := pb.Build()
  fmt.Println(*person)
}