#include "StdAfx.h"
#include "TextRenderer.h"
#include "IBuffer.h"
#include "Viewport.h"
#include "Window.h"
#include "MenuBar.h"

void Window::ResizeFunction(GLFWwindow* window, int w, int h)
{
  self->width = w;
  self->height = h;
}

Window::Window(std::string title, int width, int height):
  width{width}, height{height}
{
  self = this; // wow, it's 2016 and we're doing this!

  if (!glfwInit())
  {
    std::cerr << "failed to init window" << std::endl;
    exit(EXIT_FAILURE);
  }

  // we want a fullscreen window, so
  const GLFWvidmode* mode = glfwGetVideoMode(glfwGetPrimaryMonitor());
    
  //handle = glfwCreateWindow(mode->width, mode->height, title.c_str(), glfwGetPrimaryMonitor(), nullptr);
  handle = glfwCreateWindow(width, height, title.c_str(), nullptr, nullptr);

  if (!handle)
  {
    glfwTerminate();
    exit(EXIT_FAILURE);
  }

  glfwMakeContextCurrent(handle);
  glfwSwapInterval(1);

  glewExperimental = true;
  auto glewError = glewInit();
  if (glewError != GLEW_OK)
  {
    std::cerr << "glew init error " << glewGetErrorString(glewError) << std::endl;
  }

  // show cursor over window
  glfwSetInputMode(handle, GLFW_CURSOR_NORMAL, GL_TRUE);

  // handle keyboard presses
  glfwSetKeyCallback(handle, &Window::KeyFunction);

  // handle window being resized
  glfwSetWindowSizeCallback(handle, &Window::ResizeFunction);

  const auto ver = glGetString(GL_VERSION);
  std::cout << "graphics driver " << ver << std::endl;

  const auto sh = glGetString(GL_SHADING_LANGUAGE_VERSION);
  std::cout << "glsl version " << sh << std::endl; // do we care?

  // enable alpha blending and depth testing
  //glEnable(GL_DEPTH_TEST);
  //glDepthFunc(GL_LESS);

  glEnable(GL_BLEND);
  glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

  text_renderer = std::make_shared<TextRenderer>();

  
}

void Window::DrawEverything()
{
  // go through each of the available characters
  size_t cx = width / text_renderer->char_width;
  size_t cy = height / text_renderer->char_height;

  // scaling parameters, again :|
  auto sx = 2.0 / width;
  auto sy = 2.0 / height;

  auto half_w = width / 2.f;
  auto half_h = height / 2.f;

  for (size_t y = 0; y < cy; ++y)
  {
    for (size_t x = 0; x < cx; ++x)
    {
      for (auto v : viewports)
      {
        char c;
        bool bold;
        std::tie(c, bold) = (*v)(x, y);
        if (c)
        {
          
          float xx = (x * text_renderer->char_width - half_w) / half_w;
          float yy = -((y+1) * (text_renderer->char_height) - half_h) / half_h;
          text_renderer->DrawText(boost::lexical_cast<std::string>(c).c_str(), xx, yy, sx, sy, bold);
        }
      }
    }
  }
}

void Window::Show()
{
  while (!glfwWindowShouldClose(handle))
  {
    glUseProgram(text_renderer->program);

    // get viewport size
    glfwGetFramebufferSize(handle, &width, &height);

    // set the viewport
    glViewport(0, 0, width, height);

    glClearColor(0,0,0,0);
    glClear(GL_COLOR_BUFFER_BIT);

    // enable blending
    glEnable(GL_BLEND);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

    GLfloat black[4] = { 1,1,1,1 };

    // set font size and color
    FT_Set_Pixel_Sizes(text_renderer->normal_face, 0, 18);
    glUniform4fv(text_renderer->uniform_color, 1, black);

    // coordinates range from -1 to 1 in either direction
    
    DrawEverything();

    glfwSwapBuffers(handle);

    glfwPollEvents();
  }
}

void Window::KeyFunction(GLFWwindow* handle, int key, int scancode, int action, int mods)
{
  if (key == GLFW_KEY_ESCAPE)
  {
    glfwSetWindowShouldClose(handle, GL_TRUE);
  }

  std::string s{};
  s += std::string("key is ") +
    boost::lexical_cast<std::string>(key);
  s += std::string(", modifier is ") +
    boost::lexical_cast<std::string>(mods);
  if (action == GLFW_PRESS)
    s += " pressed";
  else if (action == GLFW_RELEASE)
    s += " released";
  self->buffers[0]->add_string(s, key%2 == 0);
}

std::pair<int, int> Window::size() const
{
  int width, height;
  glfwGetFramebufferSize(handle, &width, &height);
  return std::make_pair(width, height);
}

Window::~Window()
{
  glfwDestroyWindow(handle);
}

Window* Window::self;