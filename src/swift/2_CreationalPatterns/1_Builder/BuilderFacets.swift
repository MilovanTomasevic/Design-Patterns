class Person : CustomStringConvertible
{
  // address
  var streetAddress = "", postcode = "", city = ""

  // employment
  var companyName = "", position = ""
  var annualIncome = 0

  var description: String {
    return "I live at \(streetAddress), \(postcode), \(city). " +
      "I work at \(companyName) as a \(position) earning \(annualIncome)."
  }
}

class PersonBuilder
{
  var person = Person()
  var lives : PersonAddressBuilder {
    return PersonAddressBuilder(person)
  }
  var works : PersonJobBuilder {
    return PersonJobBuilder(person)
  }

  // no implicit conversions, so...
  func build() -> Person
  {
    return person
  }
}

class PersonJobBuilder : PersonBuilder
{
  internal init(_ person:Person)
  {
    super.init()
    self.person = person
  }
  func at(_ companyName: String) -> PersonJobBuilder
  {
    person.companyName = companyName
    return self 
  }
  func asA(_ position: String) -> PersonJobBuilder
  {
    person.position = position
    return self
  }
  func earning(_ annualIncome: Int) -> PersonJobBuilder
  {
    person.annualIncome = annualIncome
    return self
  }
}

class PersonAddressBuilder : PersonBuilder
{
  internal init(_ person: Person)
  {
    super.init()
    self.person = person
  }
  func at(_ streetAddress: String) -> PersonAddressBuilder
  {
    person.streetAddress = streetAddress
    return self
  }
  func withPostcode(_ postcode: String) -> PersonAddressBuilder
  {
    person.postcode = postcode
    return self
  }
  func inCity(_ city: String) -> PersonAddressBuilder
  {
    person.city = city
    return self
  }
}

func main()
{
  let pb = PersonBuilder()
  let p = pb
    .lives.at("123 London Road").inCity("London").withPostcode("SW12BC")
    .works.at("Fabrikam").asA("Engineer").earning(123000)
    .build();
  print(p) 
}

main()
