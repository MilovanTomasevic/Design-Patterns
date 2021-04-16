import Foundation
import XCTest

class Sentence : CustomStringConvertible
{
  var words: [String]
  var tokens = [Int: WordToken]()

  init(_ plainText: String)
  {
    words = plainText.components(separatedBy: " ")
  }

  subscript(index: Int) -> WordToken
  {
    get
    {
      let wt = WordToken()
      tokens[index] = wt
      return tokens[index]!
    }
  }

  var description: String
  {
    var ws = [String]()
    for i in 0..<words.count
    {
      var w = words[i]
      if let item = tokens[i]
      {
        if (item.capitalize)
        {
          w = w.uppercased()
        }
      }
      ws.append(w)
    }
    return ws.joined(separator: " ")
  }

  class WordToken
  {
    var capitalize: Bool = false
    init(){}
    init(capitalize: Bool)
    {
      self.capitalize = capitalize
    }
  }
}

class UMBaseTestCase : XCTestCase {}

//@testable import Test

class Evaluate: UMBaseTestCase
{
  func simpleTest()
  {
    let s = Sentence("alpha beta gamma")
    s[1].capitalize = true
    XCTAssertEqual("alpha BETA gamma", s.description)
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