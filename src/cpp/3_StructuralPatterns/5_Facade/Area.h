#pragma once
#include "Viewport.h"
#include "TextBuffer.h"

/// A co-dependent buffer and viewport.
class Area
{
  std::shared_ptr<TextBuffer> textBuffer;
  Viewport viewport;
public:
  Area(uint32_t width, uint32_t height)
    : textBuffer{ std::make_shared<TextBuffer>(width, height) }, viewport{ textBuffer }
  {
  }
};
