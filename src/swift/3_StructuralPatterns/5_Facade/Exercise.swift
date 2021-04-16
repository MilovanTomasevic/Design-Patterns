import Foundation
import XCTest

class Generator
{
  func generate(_ count: Int) -> [Int]
  {
    var result = [Int]()
    for _ in 1...count
    {
      result.append(1 + random()%9)
    }
    return result
  }
}

class Splitter
{
  func split(_ array: [[Int]]) -> [[Int]]
  {
    var result = [[Int]]()
    
    let rowCount = array.count
    let colCount = array[0].count

    // get the rows
    for r in 0..<rowCount
    {
      var theRow = [Int]()
      for c in 0..<colCount
      {
        theRow.append(array[r][c])
      }
      result.append(theRow)
    }

    // get the columns
    for c in 0..<colCount
    {
      var theCol = [Int]()
      for r in 0..<rowCount
      {
        theCol.append(array[r][c])
      }
      result.append(theCol)
    }

    // get the diagonals
    var diag1 = [Int]()
    var diag2 = [Int]()
    for c in 0..<colCount
    {
      for r in 0..<rowCount
      {
        if c == r
        {
          diag1.append(array[r][c])
        }
        let r2 = rowCount - r - 1
        if c == r2
        {
          diag2.append(array[r][c])
        }
      }
    }

    result.append(diag1)
    result.append(diag2)

    return result
  }
}

class Verifier
{
  func verify(_ arrays: [[Int]]) -> Bool
  {
    let first = arrays[0].reduce(0, +)
    for arr in 1..<arrays.count
    {
      if (arrays[arr].reduce(0, +)) != first
      {
        return false
      }
    } 
    return true
  }
}

class MagicSquareGenerator
{
  func generate(_ size: Int) -> [[Int]]
  {
    let g = Generator()
    let s = Splitter()
    let v = Verifier()

    var square: [[Int]]
    repeat 
    {
      square = [[Int]]() 
      for _ in 1...size
      {
        square.append(g.generate(size))
      }
    } while !v.verify(s.split(square))
    return square
  }
}

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let gen = MagicSquareGenerator()
    let square = gen.generate(3)
    let v = Verifier()
    XCTAssert(v.verify(square), 
      "Verification failed. This is not a valid magic square.")
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