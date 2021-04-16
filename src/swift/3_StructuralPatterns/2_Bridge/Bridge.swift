import Foundation

protocol Renderer
{
  func renderCircle(_ radius: Float)
}

class VectorRenderer : Renderer
{
  func renderCircle(_ radius: Float)
  {
    print("Drawing a circle or radius \(radius)")
  }
}

class RasterRenderer : Renderer
{
  func renderCircle(_ radius: Float)
  {
    print("Drawing pixels for circle of radius \(radius)")
  }
}

protocol Shape
{
  func draw()
  func resize(_ factor: Float)
}

class Circle : Shape
{
  var radius: Float
  var renderer: Renderer

  init(_ renderer: Renderer, _ radius: Float)
  {
    self.renderer = renderer
    self.radius = radius
  }

  func draw()
  {
    renderer.renderCircle(radius)
  }

  func resize(_ factor: Float)
  {
    radius *= factor
  }
}

func main()
{
  let raster = RasterRenderer()
  let vector = VectorRenderer()
  let circle = Circle(vector, 5)
  circle.draw()
  circle.resize(2)
  circle.draw()

  // todo DI
}

main()