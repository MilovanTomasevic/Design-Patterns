package com.activemesa.structural.bridge;

import com.google.inject.AbstractModule;
import com.google.inject.Guice;
import com.google.inject.Inject;
import com.google.inject.Injector;

interface Renderer
{
  void renderCircle(float radius);
}

class VectorRenderer implements Renderer
{
  @Override
  public void renderCircle(float radius)
  {
    System.out.println("Drawing a circle of radius " + radius);
  }
}

class RasterRenderer implements Renderer
{
  @Override
  public void renderCircle(float radius)
  {
    System.out.println("Drawing pixels for a circle of radius " + radius);
  }
}

abstract class Shape
{
  protected Renderer renderer;

  public Shape(Renderer renderer)
  {
    this.renderer = renderer;
  }

  public abstract void draw();
  public abstract void resize(float factor);
}

class Circle extends Shape
{
  public float radius;

  @Inject
  public Circle(Renderer renderer)
  {
    super(renderer);
  }

  public Circle(Renderer renderer, float radius)
  {
    super(renderer);
    this.radius = radius;
  }

  @Override
  public void draw()
  {
    renderer.renderCircle(radius);
  }

  @Override
  public void resize(float factor)
  {
    radius *= factor;
  }
}

class ShapeModule extends AbstractModule
{
  @Override
  protected void configure()
  {
    bind(Renderer.class).to(VectorRenderer.class);
  }
}

class BridgeDemo
{
  public static void main(String[] args)
  {
//    RasterRenderer rasterRenderer = new RasterRenderer();
//    VectorRenderer vectorRenderer = new VectorRenderer();
//    Circle circle = new Circle(vectorRenderer, 5);
//    circle.draw();
//    circle.resize(2);
//    circle.draw();

    // todo: Google Guice
    Injector injector = Guice.createInjector(new ShapeModule());
    Circle instance = injector.getInstance(Circle.class);
    instance.radius = 3;
    instance.draw();
    instance.resize(2);
    instance.draw();
  }
}
