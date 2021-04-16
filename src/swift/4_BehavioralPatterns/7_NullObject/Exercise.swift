import Foundation
import XCTest

protocol Log
{
  var recordLimit: Int { get }
  var recordCount: Int { get set }
  func logInfo(_ message: String)
}

enum LogError : Error
{
  case recordNotUpdated
  case logSpaceExceeded
}

class Account
{
  private var log: Log

  init(_ log: Log)
  {
    self.log = log
  }

  func someOperation() throws
  {
    let c = log.recordCount
    log.logInfo("Performing an operation")
    if (c+1) != log.recordCount
    {
      throw LogError.recordNotUpdated
    }
    if log.recordCount >= log.recordLimit
    {
      throw LogError.logSpaceExceeded
    }
  }
}

class NullLog : Log
{
  var recordLimit: Int
  {
    return Int.max
  }
  var recordCount: Int = Int.min
  func logInfo(_ message: String)
  {
    recordCount += 1
  }
}

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func simgleCallTest()
  {
    let a = Account(NullLog())
    do
    {
      try a.someOperation()
    }
    catch {
      XCTAssertFalse(true, "Did not expect an exception to occur while performing operation!")
    }
  }

  func mayCallsTest()
  {
    let a = Account(NullLog())
    for _ in 1...100
    {
      do
      {
        try a.someOperation()
      }
      catch {
        XCTAssertFalse(true, "Did not expect an exception to occur while performing operation!")
        return
      }
    }
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("simgleCallTest", simgleCallTest),
      ("mayCallsTest", mayCallsTest)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()