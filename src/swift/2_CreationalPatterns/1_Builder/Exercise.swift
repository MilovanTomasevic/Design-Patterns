import XCTest
import Foundation

class Field : CustomStringConvertible
{
  var type: String = ""
  var name: String = ""
  init(called name: String, ofType type: String)
  {
    self.name = name
    self.type = type
  }
  public var description: String
  {
    return "var \(name): \(type)"
  }
}

class Class : CustomStringConvertible
{
  var name = ""
  var fields = [Field]()

  public var description: String
  {
    var s = ""
    s.append("class \(name)\n{\n")
    for f in fields
    {
      s.append("  \(f)\n")
    }
    s.append("}\n")
    return s
  }
}

class CodeBuilder : CustomStringConvertible
{
  private var theClass = Class()

  init(_ rootName: String)
  {
    theClass.name = rootName
  }

  func addField(called name: String, ofType type: String) -> CodeBuilder
  {
    theClass.fields.append(Field(called: name, ofType: type))
    return self
  }

  public var description: String
  {
    return theClass.description
  }
}

class UMBaseTestCase : XCTestCase {}

//@testable import Test

class Evaluate: UMBaseTestCase
{

  private func process(_ s: String) -> String
  {
    return s.trimmingCharacters(in: .whitespacesAndNewlines)
  }

  func emptyTest()
  {
    let cb = CodeBuilder("Foo")
    XCTAssertEqual("class Foo\n{\n}",
      process(cb.description))
  }

  func personTest()
  {
    var cb = CodeBuilder("Person")
      .addField(called: "name", ofType: "String")
      .addField(called: "age", ofType: "Int")
    XCTAssertEqual("class Person\n{\n  var name: String\n  var age: Int\n}",
      process(cb.description))
    print(cb.description)
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("emptyTest", emptyTest),
      ("personTest", personTest)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()