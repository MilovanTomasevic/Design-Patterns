import Foundation

protocol Shape : CustomStringConvertible
{
  init() // requited for construction
  var description: String { get }
}

class Circle : Shape
{
  private var radius: Float = 0

  required init() {}
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

  required init() {}
  init(_ side: Float)
  {
    self.side = side
  }

  public var description: String
  {
    return "A square with side \(side)"
  }
}

class ColoredShape<T> : Shape where T : Shape
{
  private var color: String = "black"
  private var shape: T = T()

  required init() {}
  init(_ color: String)
  {
    self.color = color
  }

  public var description: String
  {
    return "\(shape.description) has the color \(color)"
  }
}

class TransparentShape<T> : Shape where T : Shape
{
  private var transparency: Float = 0
  private var shape: T = T()
  
  required init(){}
  init(_ transparency: Float)
  {
    self.transparency = transparency
  }

  public var description: String
  {
    return "\(shape.description) has transparency \(transparency*100)%"
  }
}

func main()
{
  let blueCircle: ColoredShape<Circle> = ColoredShape<Circle>("blue")
  print(blueCircle)

  // only transparency propagates, color does not
  let blackHalfSquare = TransparentShape<ColoredShape<Square>>(0.4)
  print(blackHalfSquare)
}

main()