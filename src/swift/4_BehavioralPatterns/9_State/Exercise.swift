import Foundation
import XCTest

class CombinationLock
{
  private let combination: [Int]
  var status = ""
  private var digitsEntered = 0
  private var failed = false

  init(_ combination: [Int])
  {
    self.combination = combination
    reset()
  }

  private func reset()
  {
    status = "LOCKED"
    digitsEntered = 0
    failed = false
  }

  func enterDigit(_ digit: Int)
  {
    if (status == "LOCKED") { status = "" }
    status += String(digit)
    if combination[digitsEntered] != digit
    {
      failed = true
    }
    digitsEntered += 1

    if digitsEntered == combination.count
    {
      status = (failed ? "ERROR" : "OPEN")
    }
  }
}

// ===

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func testSuccess()
  {
    let cl = CombinationLock([1,2,3,4,5])
    XCTAssertEqual("LOCKED", cl.status)
    cl.enterDigit(1)
    XCTAssertEqual("1", cl.status)
    cl.enterDigit(2)
    XCTAssertEqual("12", cl.status)
    cl.enterDigit(3)
    XCTAssertEqual("123", cl.status)
    cl.enterDigit(4)
    XCTAssertEqual("1234", cl.status)
    cl.enterDigit(5)
    XCTAssertEqual("OPEN", cl.status)
  }

  func testFailure()
  {
    let cl = CombinationLock([1,2,3])
    XCTAssertEqual("LOCKED", cl.status)
    cl.enterDigit(1)
    XCTAssertEqual("1", cl.status)
    cl.enterDigit(2)
    XCTAssertEqual("12", cl.status)
    cl.enterDigit(5)
    XCTAssertEqual("ERROR", cl.status)
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("testSuccess", testSuccess),
      ("testFailure", testFailure),
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()