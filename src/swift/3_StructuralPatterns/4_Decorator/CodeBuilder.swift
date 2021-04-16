import Foundation

// cannot inherit from a string
class CodeBuilder : CustomStringConvertible
{
  private var buffer: String = ""

  init(){}
  init(_ buffer: String)
  {
    self.buffer = buffer
  }

  func clear()
  {
    buffer = ""
  }

  // interface replicating the original
  // fluent interface
  func append(_ s: String) -> CodeBuilder
  {
    buffer.append(s)
    return self
  }

  // add your own customizations
  func appendLine(_ s: String) -> CodeBuilder
  {
    buffer.append("\(s)\n")
    return self
  }

  public var description: String
  {
    return buffer
  }

  // replicate string's operator
  static func += (cb: inout CodeBuilder, s: String)
  {
    cb.buffer.append(s)
  }
}



func main()
{
  var cb = CodeBuilder()
  cb.appendLine("class Foo")
    .appendLine("{")
  
  cb += "  testing\n"
  
  cb.appendLine("}")
  print(cb)
}

main()