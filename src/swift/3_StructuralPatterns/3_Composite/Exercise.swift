import Foundation
import XCTest

class SingleValue : Sequence
{
  var value = 0

  init() {}
  init(_ value: Int)
  {
    self.value = value
  }

  func makeIterator() -> IndexingIterator<Array<Int>>
  {
    return IndexingIterator(_elements: [value])
  }
}

class ManyValues : Sequence
{
  var values = [Int]()

  func makeIterator() -> IndexingIterator<Array<Int>>
  {
    return IndexingIterator(_elements: values)
  }

  func add(_ value: Int)
  {
    values.append(value)
  }
}

extension Sequence where Iterator.Element : Sequence,
  Iterator.Element.Iterator.Element == Int
{
  func sum() -> Int
  {
    var result = 0
    for c in self
    {
      for i in c
      {
        result += i
      }
    }
    return result
  }
}

class UMBaseTestCase : XCTestCase {}

//@testable import Test

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let singleValue = SingleValue(11)
    let otherValues = ManyValues()
    otherValues.add(22)
    otherValues.add(33)
    XCTAssertEqual(66,
      [AnySequence(singleValue), AnySequence(otherValues)].sum())
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