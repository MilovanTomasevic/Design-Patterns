import Foundation

protocol Shape : CustomStringConvertible
{
  var description: String { get }
}

class Circle : Shape
{
  private var radius: Float = 0

  init(_ radius: Float)
  {
    self.radius = radius
  }

  func resize(_ factor: Float)
  {
    radius *= factor
  }

  public var description: String
  {
    return "A circle of radius \(radius)"
  }
}

class Square : Shape
{
  private var side: Float = 0

  init(_ side: Float)
  {
    self.side = side
  }

  public var description: String
  {
    return "A square with side \(side)"
  }
}

class ColoredShape : Shape
{
  var shape: Shape
  var color: String

  init(_ shape: Shape, _ color: String)
  {
    self.shape = shape
    self.color = color
  }

  var description: String
  {
    return "\(shape.description) has color \(color)"
  }
}

class TransparentShape : Shape
{
  var shape: Shape
  var transparency: Float

  init(_ shape: Shape, _ transparency: Float)
  {
    self.shape = shape
    self.transparency = transparency
  }

  var description: String
  {
    return "\(shape.description) has \(transparency*100)% transparency"
  }
}

func main()
{
  // dynamic
  let square = Square(1.23)
  print(square)

  let redSquare = ColoredShape(square, "red")
  print(redSquare)

  let redHalfTransparentSquare = TransparentShape(redSquare, 0.5)
  print(redHalfTransparentSquare)
}

main()