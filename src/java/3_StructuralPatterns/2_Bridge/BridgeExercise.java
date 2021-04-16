package com.activemesa.structural.bridge.exercise;

//abstract class Shape
//{
//  public abstract String getName();
//}
//
//class Triangle extends Shape
//{
//  @Override
//  public String getName()
//  {
//    return "Triangle";
//  }
//}
//
//class Square extends Shape
//{
//  @Override
//  public String getName()
//  {
//    return "Square";
//  }
//}
//
//class VectorSquare extends Square
//{
//  @Override
//  public String toString()
//  {
//    return String.format("Drawing %s as lines", getName());
//  }
//}
//
//class RasterSquare extends Square
//{
//  @Override
//  public String toString()
//  {
//    return String.format("Drawing %s as pixels", getName());
//  }
//}

// imagine VectorTriangle and RasterTriangle are here too

interface Renderer
{
  public String whatToRenderAs();
}

abstract class Shape
{
  private Renderer renderer;
  public String name;

  public Shape(Renderer renderer)
  {
    this.renderer = renderer;
  }

  @Override
  public String toString()
  {
    return String.format("Drawing %s as %s",
      name, renderer.whatToRenderAs());
  }
}

class Triangle extends Shape
{
  public Triangle(Renderer renderer)
  {
    super(renderer);
    name = "Triangle";
  }
}

class Square extends Shape
{
  public Square(Renderer renderer)
  {
    super(renderer);
    name = "Square";
  }
}

class RasterRenderer implements Renderer
{

  @Override
  public String whatToRenderAs()
  {
    return "pixels";
  }
}

class VectorRenderer implements Renderer
{
  @Override
  public String whatToRenderAs()
  {
    return "lines";
  }
}