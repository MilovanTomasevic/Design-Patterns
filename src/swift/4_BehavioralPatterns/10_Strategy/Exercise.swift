import Foundation
import XCTest

protocol DiscriminantStrategy
{
  func calculateDiscriminant(_ a: Double, _ b: Double, _ c: Double) -> Double
}

class OrdinaryDiscriminantStrategy : DiscriminantStrategy
{
  func calculateDiscriminant(_ a: Double, _ b: Double, _ c: Double) -> Double
  {
    return b*b - 4*a*c
  }
}

class RealDiscriminantStrategy : DiscriminantStrategy
{
  func calculateDiscriminant(_ a: Double, _ b: Double, _ c: Double) -> Double
  {
    let result = b*b - 4*a*c
    return (result >= 0) ? result : Double.nan
  }
}

class QuadraticEquationSolver
{
  private let strategy: DiscriminantStrategy

  init(_ strategy: DiscriminantStrategy)
  {
    self.strategy = strategy
  }

  func solve(_ a: Double, _ b: Double, _ c: Double) -> (Double, Double)
  {
    let disc = strategy.calculateDiscriminant(a, b, c)
    let rootDisc = sqrt(disc)
    return ((-b + rootDisc) / (2*a), (-b - rootDisc) / (2*a))
  }
}

// ===

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func positiveTestOrdinaryStrategy()
  {
    let strategy = OrdinaryDiscriminantStrategy()
    let solver = QuadraticEquationSolver(strategy)
    let (x1, x2) = solver.solve(1, 10, 16)

    XCTAssertEqual(-2, x1)
    XCTAssertEqual(-8, x2)
  }

  func negativeTestRealStrategy()
  {
    let strategy = RealDiscriminantStrategy()
    let solver = QuadraticEquationSolver(strategy)
    let (x1, x2) = solver.solve(1, 4, 5)

    XCTAssert(x1.isNaN)
    XCTAssert(x2.isNaN)
  }
}

extension Evaluate
{
  static var allTests : [(String, (Evaluate) -> () throws -> Void)]
  {
    return [
      ("positiveTestOrdinaryStrategy", positiveTestOrdinaryStrategy),
      ("negativeTestRealStrategy", negativeTestRealStrategy)
    ]
  }
}

func main()
{
  XCTMain([testCase(Evaluate.allTests)])
}

main()