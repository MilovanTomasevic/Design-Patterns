import Foundation
import XCTest

protocol Expression
{

}

class DoubleExpression : Expression
{
  var value: Double
  init(_ value: Double)
  {
    self.value = value
  }
}

class AdditionExpression : Expression
{
  let left, right: Expression

  init(_ left: Expression, _ right: Expression)
  {
    self.left = left
    self.right = right
  }
}

class ExpressionPrinter
{
  // we don't want to use 'self' before initialization
  // so this is marked as 'lazy'
  lazy var actions: [String: (Expression, inout String) -> ()] = [
    "Reflective.DoubleExpression": {
      let de = $0 as! DoubleExpression
      $1.append(String(de.value))
    },
    "Reflective.AdditionExpression": {
      let ae = $0 as! AdditionExpression
      $1.append("(")
      self.print2(ae.left, &$1)
      $1.append("+")
      self.print2(ae.right, &$1)
      $1.append(")")
    }
  ]

  func print(_ e: Expression, _ s: inout String)
  {
    if let de = e as? DoubleExpression
    {
      s.append(String(de.value))
    }
    else if let ae = e as? AdditionExpression
    {
      s.append("(")
      print(ae.left, &s)
      s.append("+")
      print(ae.right, &s)
      s.append(")")
    }
  }

  // breaks open-closed principle
  // will work incorrectly on missing case

  func print2(_ e: Expression, _ s: inout String)
  {
    if let handler = actions[String(describing: e.self)]
    {
      handler(e, &s)
    }
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
  var s = ""
  let ep = ExpressionPrinter()
  ep.print(e, &s)
  print(s)

  // try the more concise approach
  s = ""
  ep.print2(e, &s)
  print(s)
}

main()