// class Shape
// {
//   constructor(name)
//   {
//     this.name = name;
//   }
// }
//
// class Triangle extends Shape
// {
//   constructor()
//   {
//     super('triangle');
//   }
// }
//
// class Square extends Shape
// {
//   constructor()
//   {
//     super('square');
//   }
// }
//
// class VectorSquare extends Square
// {
//   toString()
//   {
//     return `Drawing square as lines`;
//   }
// }
//
// class RasterSquare extends Square
// {
//   toString()
//   {
//     return `Drawing square as pixels`;
//   }
// }

// imagine VectorTriangle and RasterTriangle are here too

class Shape
{
  constructor(renderer, name=null)
  {
    this.renderer = renderer;
    this.name = name;
  }

  toString()
  {
    return `Drawing ${this.name} as ${this.renderer.whatToRenderAs}`;
  }
}

class Triangle extends Shape
{
  constructor(renderer)
  {
    super(renderer, 'triangle');
  }
}

class Square extends Shape
{
  constructor(renderer)
  {
    super(renderer, 'square');
  }
}

class RasterRenderer
{
  get whatToRenderAs()
  {
    return 'pixels';
  }
}

class VectorRenderer
{
  get whatToRenderAs()
  {
    return 'lines';
  }
}

describe('facade', function()
{
  it('render vector square', function()
  {
    let sq = new Square(new VectorRenderer());
    expect(sq.toString()).toEqual('Drawing square as lines');
  });

  it('render raster triangle', function()
  {
    let sq = new Triangle(new RasterRenderer());
    expect(sq.toString()).toEqual('Drawing triangle as pixels');
  });
});