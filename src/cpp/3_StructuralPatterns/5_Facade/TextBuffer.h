#pragma once
#include "IBuffer.h"
#include <vector>
#include <boost/circular_buffer.hpp>
#include <boost/optional/optional.hpp>

class TextBuffer : public IBuffer
{
  boost::circular_buffer<std::string> lines;
  const std::string line_break = "\n";
  uint32_t width, height;
  boost::circular_buffer<bool> format;
public:
  TextBuffer(uint32_t width, uint32_t height);
  Size get_size() const override;
  std::string to_string();
  char operator()(uint32_t x, uint32_t y) override;
  void add_string(const std::string& s, bool bold) override;
  bool get_format(int line) override;
};
