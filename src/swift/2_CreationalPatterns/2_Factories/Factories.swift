import Foundation

class Point : CustomStringConvertible
{
  var x, y: Double

  init(x: Double, y: Double)
  {
    self.x = x
    self.y = y
  }

  init(rho: Double, theta: Double)
  {
    x = rho * cos(theta)
    y = rho * sin(theta)
  }

  public var description: String
  {
    return "x = \(x), y = \(y)"
  }

  // factory method
  static func createCartesian
    (x: Double, y: Double) -> Point
  {
    return Point(x:x, y:y)
  }

  static func createPolar
    (rho: Double, theta: Double) -> Point
  {
    return Point(rho:rho, theta:theta)
  }
}

class PointFactory
{
  func createCartesian
    (x: Double, y: Double) -> Point
  {
    return Point(x:x, y:y)
  }

  // feel free to make it static
  static func createPolar
    (rho: Double, theta: Double) -> Point
  {
    return Point(rho:rho, theta:theta)
  }
}

class ShadyPoint : CustomStringConvertible 
{
  private var x, y: Double

  private init(x: Double, y: Double)
  {
    self.x = x
    self.y = y
  }

  private init(rho: Double, theta: Double)
  {
    x = rho * cos(theta)
    y = rho * sin(theta)
  }

  public var description: String
  {
    return "x = \(x), y = \(y)"
  }

  // factory method
  static func createCartesian
    (x: Double, y: Double) -> Point
  {
    return Point(x:x, y:y)
  }

  static func createPolar
    (rho: Double, theta: Double) -> Point
  {
    return Point(rho:rho, theta:theta)
  }

  //static let factory = ShadyPointFactory()
  static let factory = ShadyPointFactory.instance

  class ShadyPointFactory
  {
    // singleton
    private init() {}
    static let instance = ShadyPointFactory()

    func createCartesian
      (x: Double, y: Double) -> ShadyPoint
    {
      return ShadyPoint(x:x, y:y)
    }

    static func createPolar
      (rho: Double, theta: Double) -> ShadyPoint
    {
      return ShadyPoint(rho:rho, theta:theta)
    }
  }
}

func main()
{
  let p = Point.createPolar(rho:1,theta:2)
  print(p)

  // can instantiate factory
  let f = PointFactory()
  let p2 = f.createCartesian(x:2,y:1)
  print(p2)

  // or use it statically
  let p3 = PointFactory.createPolar(rho: 3, theta: Double.pi/3)
  print(p3)

  // cannot initialize ShadyPoint directly
  //let sp = ShadyPoint(x:1,y:2)

  // cannot make an external factory, unless...

  //let spf = ShadyPoint.ShadyPointFactory()
  //let p4 = spf.createCartesian(x:1, y:2)
  //print(p4)

  let p5 = ShadyPoint.ShadyPointFactory.createPolar(rho: 1, theta: Double.pi)
  print(p5)

  // make a point through factory
  let p6 = ShadyPoint.factory.createCartesian(x:1, y:2)
  print(p6)

  // but the factory itself is usable, maybe you don't want this
}

main()
