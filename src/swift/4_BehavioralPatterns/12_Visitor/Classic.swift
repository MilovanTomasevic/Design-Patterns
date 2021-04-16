import Foundation

protocol Expression
{
  func accept(_ visitor: ExpressionVisitor)
}

class DoubleExpression : Expression
{
  var value: Double

  init(_ value: Double)
  {
    self.value = value
  }

  func accept(_ visitor: ExpressionVisitor)
  {
    visitor.visit(self)
  }
}

class AdditionExpression : Expression
{
  var left: Expression
  var right: Expression

  init(_ left: Expression, _ right: Expression)
  {
    self.left = left
    self.right = right
  }

  func accept(_ visitor: ExpressionVisitor)
  {
    visitor.visit(self)
  }
}

protocol ExpressionVisitor
{
  func visit(_ de: DoubleExpression)
  func visit(_ ae: AdditionExpression)
}

class ExpressionPrinter : ExpressionVisitor, CustomStringConvertible
{
  private var buffer = ""

  func visit(_ de: DoubleExpression)
  {
    buffer.append(String(de.value))
  }

  func visit(_ ae: AdditionExpression)
  {
    buffer.append("(")
    ae.left.accept(self)
    buffer.append("+")
    ae.right.accept(self)
    buffer.append(")")
  }

  public var description: String {
    return buffer
  }
}

class ExpressionCalculator : ExpressionVisitor
{
  var result = 0.0

  func visit(_ de: DoubleExpression)
  {
    result = de.value
  }

  func visit(_ ae: AdditionExpression)
  {
    ae.left.accept(self)
    let a = result
    ae.right.accept(self)
    let b = result
    result = a + b
  }
}

func main()
{
  let e = AdditionExpression(
    DoubleExpression(1),
    AdditionExpression(
      DoubleExpression(2),
      DoubleExpression(3)
    )
  )
  let ep = ExpressionPrinter()
  ep.visit(e)
  print(ep)

  let calc = ExpressionCalculator()
  calc.visit(e)
  print("\(ep) = \(calc.result)")
}

main()