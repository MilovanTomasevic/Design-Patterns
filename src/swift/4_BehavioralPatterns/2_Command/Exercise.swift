import Foundation
import XCTest

class Command
{
  enum Action
  {
    case deposit
    case withdraw
  }

  var action: Action
  var amount: Int
  var success = false

  init(_ action: Action, _ amount: Int)
  {
    self.action = action
    self.amount = amount
  }
}

class Account
{
  var balance = 0

  func process(_ c: Command)
  {
    switch c.action
    {
      case .deposit:
        balance += c.amount
        c.success = true
      case .withdraw:
        c.success = (balance >= c.amount)
        if (c.success) { balance -= c.amount }
    }
  }
}

class UMBaseTestCase : XCTestCase {}

//@testable import Test

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let a = Account()
    
    let deposit = Command(.deposit, 100)
    a.process(deposit)

    XCTAssertEqual(100, a.balance, "Expected the balance to rise to 100")
    XCTAssert(deposit.success, "A deposit should always succeed")

    let withdraw = Command(.withdraw, 50)
    a.process(withdraw)

    XCTAssertEqual(50, a.balance)
    XCTAssert(withdraw.success, "Withdrawal of 50 should have succeeded")

    let withdraw2 = Command(.withdraw, 100)
    a.process(withdraw2)

    XCTAssertEqual(50, a.balance, "After a failed withdrawal, balance should have stayed at exactly 50")
    XCTAssertFalse(withdraw2.success, "Attempted withdrawal should have failed")
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