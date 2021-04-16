import Foundation
import XCTest

class Person
{
  var id: Int
  var name: String

  init(called name: String, withId id: Int)
  {
    self.name = name
    self.id = id
  }
}

class PersonFactory
{
  private var id = 0

  func createPerson(name: String) -> Person
  {
    let p = Person(called: name, withId: id)
    id += 1
    return p
  }
}

class UMBaseTestCase : XCTestCase {}

//@testable import Test

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let pf = PersonFactory()

    let p1 = pf.createPerson(name: "Chris")
    XCTAssertEqual("Chris", p1.name)

    let p2 = pf.createPerson(name: "Sarah")
    XCTAssertEqual(1, p2.id)
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