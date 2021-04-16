import Foundation
import XCTest

protocol ExpressionVisitor
{
  func accept(_ value: Value)
  func accept(_ ae: AdditionExpression)
  func accept(_ me: MultiplicationExpression)
}

protocol Expression
{
  func visit(_ ev: ExpressionVisitor)
}

class Value : Expression
{
  let value: Int
  init(_ value: Int)
  {
    self.value = value
  }
  func visit(_ ev: ExpressionVisitor)
  {
    ev.accept(self)
  }
}

class AdditionExpression : Expression
{
  let lhs, rhs: Expression
  init(_ lhs: Expression, _ rhs: Expression)
  {
    self.lhs = lhs
    self.rhs = rhs
  }
  func visit(_ ev: ExpressionVisitor)
  {
    ev.accept(self)
  }
}

class MultiplicationExpression : Expression
{
  let lhs, rhs: Expression
  init(_ lhs: Expression, _ rhs: Expression)
  {
    self.lhs = lhs
    self.rhs = rhs
  }
  func visit(_ ev: ExpressionVisitor)
  {
    ev.accept(self)
  }
}

class ExpressionPrinter :
  ExpressionVisitor, CustomStringConvertible
{
  private var buffer = ""

  func accept(_ value: Value)
  {
    buffer.append(String(value.value))
  }

  func accept(_ ae: AdditionExpression)
  {
    buffer.append("(")
    ae.lhs.visit(self)
    buffer.append("+")
    ae.rhs.visit(self)
    buffer.append(")")
  }

  func accept(_ me: MultiplicationExpression)
  {
    me.lhs.visit(self)
    buffer.append("*")
    me.rhs.visit(self)
  }

  var description: String
  {
    return buffer
  }
}

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func simpleAddition()
  {
    let simple = AdditionExpression(
      Value(2), Value(3)
    )
    let ep = ExpressionPrinter()
    ep.accept(simple)
    XCTAssertEqual("(2+3)", ep.description)
  }

  func productOfAdditionAndValue()
  {
    let expr = MultiplicationExpression(
      AdditionExpression(Value(2), Value(3)),
      Value(4)
    )
    let ep = ExpressionPrinter()
    ep.accept(expr)
    XCTAssertEqual("(2+3)*4", ep.description)
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("simpleAddition", simpleAddition),
      ("productOfAdditionAndValue", productOfAdditionAndValue)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()