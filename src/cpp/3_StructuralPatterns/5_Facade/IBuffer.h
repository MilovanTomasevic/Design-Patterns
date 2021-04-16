#pragma once
#include "Util.h"

class IBuffer
{
public:
  virtual ~IBuffer()
  {
  }
  virtual Size get_size() const = 0;
  virtual char operator()(uint32_t x, uint32_t y) = 0;
  virtual void add_string(const std::string& s, bool bold) = 0;
  virtual bool get_format(int line) = 0;
};