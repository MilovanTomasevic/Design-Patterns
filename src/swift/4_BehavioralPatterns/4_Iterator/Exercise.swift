import Foundation
import XCTest

class Node<T>
{
  let value: T
  var left: Node<T>? = nil
  var right: Node<T>? = nil
  var parent: Node<T>? = nil

  init(_ value: T)
  {
    self.value = value
  }

  init(_ value: T, _ left: Node<T>, _ right: Node<T>)
  {
    self.value = value
    self.left = left
    self.right = right

    left.parent = self
    right.parent = self
  }

  private func traverse(_ current: Node<T>, _ buffer: inout [T])
  {
    // not the optimal solution, but it works
    buffer.append(current.value)

    if let currentLeft = current.left
    {
      traverse(currentLeft, &buffer)
    }

    if let currentRight = current.right
    {
      traverse(currentRight, &buffer)
    }
  }

  public var preOrder: [T]
  {
    get {
      var buffer = [T]()
      traverse(self, &buffer)
      return buffer
    }
  }
}

class UMBaseTestCase : XCTestCase {}

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let node = Node("a",
      Node("b",
        Node("c"),
        Node("d")
      ),
      Node("e")
    )
    XCTAssertEqual("abcde", node.preOrder.joined(),
      "Preorder traversal of this structure should yield 'abcde'")
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