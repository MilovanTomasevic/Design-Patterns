import Foundation
import XCTest

class Token
{
  var value = 0
  init(_ value: Int)
  {
    self.value = value
  }
  init(copyFrom other: Token)
  {
    self.value = other.value
  }
  static func ==(_ lhs: Token, _ rhs: Token) -> Bool
  {
    return lhs.value == rhs.value
  }
}

class Memento
{
  var tokens = [Token]()
}

class TokenMachine
{
  var tokens = [Token]()

  func addToken(_ value: Int) -> Memento
  {
    tokens.append(Token(value))
    let m = Memento()
    m.tokens = tokens.map{Token(copyFrom: $0)}
    return m
  }

  func addToken(_ token: Token) -> Memento
  {
    tokens.append(token)
    let m = Memento()
    m.tokens = tokens.map{Token(copyFrom: $0)}
    return m
  }

  func revert(to m: Memento)
  {
    tokens = m.tokens.map{ Token(copyFrom: $0) }
  }
}

// ===

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func singleTokenTest()
  {
    let tm = TokenMachine()
    let m = tm.addToken(123)
    let _ = tm.addToken(456)
    tm.revert(to: m)
    XCTAssertEqual(1, tm.tokens.count)
    XCTAssert(Token(123) == tm.tokens[0])
  }

  func twoTokenTest()
  {
    let tm = TokenMachine()
    let _ = tm.addToken(1)
    let m = tm.addToken(2)
    let _ = tm.addToken(3)
    tm.revert(to: m)
    XCTAssertEqual(2, tm.tokens.count)
    XCTAssert(Token(1) == tm.tokens[0])
    XCTAssert(Token(2) == tm.tokens[1])
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("singleTokenTest", singleTokenTest),
      ("twoTokenTest", twoTokenTest)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()