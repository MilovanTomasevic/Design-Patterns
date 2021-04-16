import Foundation

class Rectangle : CustomStringConvertible
{
  internal var _width: Int = 0
  internal var _height: Int = 0
  var width: Int
  {
    get {
      return _width
    }
    set(value) {
      _width = value
    }
  }
  var height: Int
  {
    get {
      return _height
    }
    set(value) {
      _height = value
    }
  }

  init(_ width: Int, _ height: Int)
  {
    self.width = width
    self.height = height
  }

  public var description: String
  {
    return "Width: \(width), height: \(height)"
  }
}

class Square : Rectangle
{
  var height: Int
  {
    get { return _height }
    set(value)
    {
      _width = value
      _height = value
    }
  }
  var width: Int
  {
    get { return _width }
    set(value)
    {
      _width = value
      _height = value
    }
  }
}

func area(_ r : Rectangle) -> Int {
  return r.width * r.height
}

func main()
{
  var rc = Rectangle(2,3)
  print("\(rc) has area \(area(rc))")

  // should be able to substitute a base type for a subtype
  var sq: Rectangle = Square(0,0)
  sq.width = 4
  print("\(sq) has area \(area(sq))")
}

main()