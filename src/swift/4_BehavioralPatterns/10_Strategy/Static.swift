import Foundation
import XCTest

enum OutputFormat
{
  case markdown
  case html
}

protocol ListStrategy
{
  init()
  func start(_ buffer: inout String)
  func end(_ buffer: inout String)
  func addListItem(buffer: inout String, item: String)
}

class MarkdownListStrategy : ListStrategy
{
  required init() {}
  func start(_ buffer: inout String) {}
  func end(_ buffer: inout String) {}
  func addListItem(buffer: inout String, item: String)
  {
    buffer.append(" * \(item)\n")
  }
}

class HtmlListStrategy : ListStrategy
{
  required init() {}
  func start(_ buffer: inout String)
  {
    buffer.append("<ul>\n")
  }

  func end(_ buffer: inout String)
  {
    buffer.append("</ul>\n")
  }

  func addListItem(buffer: inout String, item: String)
  {
    buffer.append("  <li>\(item)</li>\n")
  }
}

class TextProcessor<LS> : CustomStringConvertible
  where LS : ListStrategy
{
  private var buffer = ""
  private let listStrategy = LS()

  func appendList(_ items: [String])
  {
    listStrategy.start(&buffer)
    for item in items
    {
      listStrategy.addListItem(buffer: &buffer, item: item)
    }
    listStrategy.end(&buffer)
  }

  var description: String
  {
    return buffer
  }
}

func main()
{
  // you cannot change the behavior of 'tp' after it's declared
  let tp = TextProcessor<MarkdownListStrategy>()
  tp.appendList(["foo", "bar", "baz"])
  print(tp.description)

  let tp2 = TextProcessor<HtmlListStrategy>()
  tp2.appendList(["foo", "bar", "baz"])
  print(tp2.description)
}

main()