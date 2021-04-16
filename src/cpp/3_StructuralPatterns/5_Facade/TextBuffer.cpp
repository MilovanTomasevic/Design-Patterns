#include "StdAfx.h"
#include "TextBuffer.h"

TextBuffer::TextBuffer(uint32_t width, uint32_t height)
  : lines{ height }, format{ height }, width { width }, height{ height }
{
  
}

Size TextBuffer::get_size() const
{
  return{ width, height };
}

std::string TextBuffer::to_string()
{
  std::ostringstream oss;
  for (auto& line : lines)
    oss << line << line_break;
  return oss.str();
}

char TextBuffer::operator()(uint32_t x, uint32_t y)
{
  if (y + 1 > lines.size())
    return 0; // voidspace
  return x+1 > lines[y].size() ? 0 : lines[y][x];
}

void TextBuffer::add_string(const std::string& s, bool bold)
{
  lines.push_back(s);
  format.push_back(bold);
}

bool TextBuffer::get_format(int line)
{
  if (line + 1 > format.size()) return false;
  return format[line];
}
