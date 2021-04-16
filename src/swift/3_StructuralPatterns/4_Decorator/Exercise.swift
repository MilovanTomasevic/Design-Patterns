import Foundation
import XCTest

class Bird
{
  var age = 0

  func fly() -> String
  {
    return (age < 10) ? "flying" : "too old"
  }
}

class Lizard
{
  var age = 0

  func crawl() -> String
  {
    return (age > 1) ? "crawling" : "too young"
  }
}

class Dragon
{
  private var _age = 0
  private let bird = Bird()
  private let lizard = Lizard()

  var age: Int
  {
    get { return _age }
    set(value)
    {
      lizard.age = value
      bird.age = value
      _age = value
    }
  }

  func fly() -> String { return bird.fly() }
  func crawl() -> String { return lizard.crawl() }
}

class UMBaseTestCase : XCTestCase {}

//@testable import Test

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let dragon = Dragon()

    XCTAssertEqual("flying", dragon.fly())
    XCTAssertEqual("too young", dragon.crawl())

    dragon.age = 20

    XCTAssertEqual("too old", dragon.fly())
    XCTAssertEqual("crawling", dragon.crawl())
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