import Foundation
import XCTest

class SingletonTester
{
  static func isSingleton(factory: () -> AnyObject) -> Bool
  {
    let obj1 = factory()
    let obj2 = factory()
    return obj1 === obj2
  }
}

class UMBaseTestCase : XCTestCase {}

//@testable import Test

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let obj = SingletonTester() // hehe, inception!
    XCTAssert(type(of: obj).isSingleton{obj})
    XCTAssertFalse(type(of: obj).isSingleton{ SingletonTester() })
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