#include "stdafx.h"
#include "MenuBar.h"

MenuBar::MenuBarBuilder MenuBar::create(uint32_t width, uint32_t height)
{
  return MenuBarBuilder{ std::make_shared<MenuBar>(width, height) };
}
