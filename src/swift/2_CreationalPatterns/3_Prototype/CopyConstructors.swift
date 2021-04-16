import Foundation

protocol Copying
{
  // the 'required' keyword is only used in classes
  init(copyFrom other: Self)
}

class Address : CustomStringConvertible, Copying
{
  var streetAddress: String
  var city: String

  init(_ streetAddress: String, _ city: String)
  {
    self.streetAddress = streetAddress
    self.city = city
  }

  required init(copyFrom other: Address)
  {
    streetAddress = other.streetAddress
    city = other.city
  }

  var description: String
  {
    return "\(streetAddress), \(city)"
  }
}

struct Employee: CustomStringConvertible, Copying
{
  var name: String
  var address: Address

  init(_ name: String, _ address: Address)
  {
    self.name = name
    self.address = address
  }

  // C++ style copy constructor
  /* required */ init(copyFrom other: Employee)
  {
    name = other.name

    // one option is to do this
    //address = Address(other.address.streetAddress, other.address.city)

    // and another option is to do this
    address = Address(copyFrom: other.address)
  }

  var description: String
  {
    return "My name is \(name) and I live at \(address)"
  }
}



func main()
{
  var john = Employee("John", Address("123 London Road", "London"))


  // if employee is a class, these refer to the same obj
  // if it's a struct, a copy is made
  //var chris = john


  var chris = Employee(copyFrom: john)
  chris.name = "Chris"
  chris.address.city = "Manchester"

  // if address is a class, they both end up in Manchester
  // if address is a struct, a copy is made

  print(chris.description)
  print(john.description)
}

main()