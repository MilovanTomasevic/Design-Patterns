class VectorRenderer
{
  renderCircle(radius)
  {
    console.log(`Drawing a circle of radius ${radius}`);
  }
}

class RasterRenderer
{
  renderCircle(radius)
  {
    console.log(`Drawing pixels for circle of radius ${radius}`);
  }
}

class Shape
{
  constructor(renderer)
  {
    this.renderer = renderer;
  }
}

class Circle extends Shape
{
  constructor(renderer, radius) {
    super(renderer);
    this.radius = radius;
  }

  draw()
  {
    this.renderer.renderCircle(this.radius);
  }

  resize(factor)
  {
    this.radius *= factor;
  }
}

// imagine Square, Triangle
// different ways of rendering: vector, raster
// we don't want a cartesian product of these

let raster = new RasterRenderer();
let vector = new VectorRenderer();
let circle = new Circle(vector, 5);
circle.draw();
circle.resize(2);
circle.draw();