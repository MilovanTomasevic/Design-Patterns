import Foundation
import XCTest

class Buffer
{
  var width, height: Int
  var buffer: [Character]
  init(_ width: Int, _ height: Int)
  {
    self.width = width
    self.height = height
    buffer = [Character](repeating: " ", count: 64)
  }

  subscript(_ index: Int) -> Character
  {
    return buffer[index]
  }
}

class Viewport
{
  var buffer: Buffer
  var offset = 0
  init(_ buffer: Buffer)
  {
    self.buffer = buffer
  }

  func getCharacterAt(_ index: Int) -> Character
  {
    return buffer[offset + index]
  }
}

class Console
{
  var buffers = [Buffer]()
  var viewports = [Viewport]()
  var offset = 0

  init()
  {
    let b = Buffer(10,10)
    let v = Viewport(b)
    buffers.append(b)
    viewports.append(v)
  }

  func getCharacterAt(_ index: Int) -> Character
  {
    // use default buffer
    return viewports[0].getCharacterAt(index)
  }
}

func main()
{
  let c = Console()
  let u = c.getCharacterAt(1)
}

main() // nothing to run here :|