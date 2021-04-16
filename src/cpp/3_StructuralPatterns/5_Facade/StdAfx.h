#include <cstdlib>
#include <iostream>
#include <sstream>
#include <algorithm>

#define GLEW_STATIC
#include <GL/glew.h>
#include <GLFW/glfw3.h>
#include <ft2build.h>
#include FT_FREETYPE_H
#include FT_LCD_FILTER_H
#include FT_UNPATENTED_HINTING_H
#include "ShaderUtils.h"

#include <boost/lexical_cast.hpp>
#include <boost/algorithm/string.hpp>