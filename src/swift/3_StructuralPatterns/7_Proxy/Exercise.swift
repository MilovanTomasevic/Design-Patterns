import Foundation
import XCTest

class Person
{
  var age: Int = 0

  func drink() -> String
  {
    return "drinking"
  }

  func drive() -> String
  {
    return "driving"
  }

  func drinkAndDrive() -> String
  {
    return "driving while drunk"
  }
}

class ResponsiblePerson
{
  private let person: Person

  init(person: Person)
  {
    self.person = person
  }

  var age: Int
  {
    get { return person.age }
    set(value) { person.age = value }
  }

  func drink() -> String
  {
    return (age >= 18) ? person.drink() : "too young"
  }

  func drive() -> String
  {
    return (age >= 16) ? person.drive() : "too young"
  }

  func drinkAndDrive() -> String
  {
    return "dead"
  }
}

class UMBaseTestCase : XCTestCase {}

//@testable import Test

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let p = Person()
    p.age = 10
    let rp = ResponsiblePerson(person: p)

    XCTAssertEqual("too young", rp.drive(), "Should be too young to drive at age 10")
    XCTAssertEqual("too young", rp.drink(), "Should be too young to drink vodka at age 10")
    XCTAssertEqual("dead", rp.drinkAndDrive(), "Drinking while driving should lead to certain death")

    rp.age = 20

    XCTAssertEqual("driving", rp.drive(), "Driving at age 20 should be OK")
    XCTAssertEqual("drinking", rp.drink(), "Drinking at age 20 should be OK so long as you do it in moderation")
    XCTAssertEqual("dead", rp.drinkAndDrive(), "Driving while drunk leads to hell")
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("simpleTest", simpleTest)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()