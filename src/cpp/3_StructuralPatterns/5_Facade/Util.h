#pragma once

#include <cstdint>

struct Size
{
  uint32_t width; 
  uint32_t height;
};

struct Point
{
  uint32_t x;
  uint32_t y;

  Point() : x(0), y(0) {}
  Point(int x, int y) : x(x), y(y) {}
};
