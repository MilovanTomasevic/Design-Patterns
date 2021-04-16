#pragma once
#include "Area.h"
#include <boost/signals2.hpp>

class MenuItem;

struct MenuItemContainer
{
  virtual ~MenuItemContainer()
  {
  }

  virtual std::vector<std::shared_ptr<MenuItem>>& get_items() = 0;
};

class MenuBar : public Area, public MenuItemContainer
{
public:
  MenuBar(uint32_t width, uint32_t height)
    : Area{ width, height }
  {
  }

  std::vector<std::shared_ptr<MenuItem>>& get_items() override
  {
    return items;
  }

  std::vector<std::shared_ptr<MenuItem>> items;
  class MenuBarBuilder;
  static MenuBarBuilder create(uint32_t width, uint32_t height);
};

class MenuItem;

class MenuItem : public MenuItemContainer
{
public:
  MenuItem(std::shared_ptr<MenuItemContainer> parent, const std::string& text)
    : parent{parent}, text{text}
  {
  }

  std::vector<std::shared_ptr<MenuItem>>& get_items() override
  {
    return items;
  }

  std::shared_ptr<MenuItemContainer> parent;
  std::string text;
  std::vector<std::shared_ptr<MenuItem>> items;
  boost::signals2::signal<bool(MenuItem&)> fired;
};

/// You cannot use this class directly, use MenuBar::create().
class MenuBar::MenuBarBuilder
{
  /// Allows MenuBar to call the private constructor
  friend class MenuBar;

  std::shared_ptr<MenuItemContainer> menu;

  explicit MenuBarBuilder(const std::shared_ptr<MenuItemContainer>& menu_item_container)
    : menu{menu_item_container}
  {
  }

public:
  MenuBarBuilder& add(std::string text, 
    std::function<bool(MenuItem&)> handler,
    std::function<void(MenuBarBuilder&)> children = {})
  {
    auto item = std::make_shared<MenuItem>(menu, text);
    item->fired.connect(handler);

    if (children)
    {
      MenuBarBuilder child_builder{ item };
      children(child_builder);
    }

    menu->get_items().emplace_back(item);

    return *this;
  }
};