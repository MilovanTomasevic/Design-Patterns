#pragma once

#include "TextRenderer.h"
#include "IBuffer.h"
#include "Viewport.h"
#include <memory>
#include <vector>

struct Window
{
  int width, height;
  GLFWwindow* handle = nullptr;
  std::shared_ptr<TextRenderer> text_renderer;

  std::vector<std::shared_ptr<IBuffer>> buffers;
  std::vector<std::shared_ptr<Viewport>> viewports;
  static void ResizeFunction(GLFWwindow*, int, int);
  
  Window(std::string title, int width, int height);

  void DrawEverything();
  void Show();
  static void KeyFunction(GLFWwindow* handle, int key, int scancode, int action, int mods);
  std::pair<int, int> size() const;

  ~Window();

  // used to get around the callback stupidity with C APIs
  static Window* self;
};
