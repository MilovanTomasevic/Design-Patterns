#pragma once

struct TextRenderer
{
  FT_Library library;
  FT_Face normal_face, italic_face;
  GLuint program;
  GLint attribute_coord;
  GLint uniform_tex;
  GLint uniform_color;
  GLuint vbo;
  size_t char_width, char_height;

  TextRenderer() { Initialize(); }

  ~TextRenderer()
  {
    glDeleteProgram(program);
  }

  /// Initializes FreeType and sets all structure fields.
  void Initialize();

  void DrawText(const char *text, float x, float y, float sx, float sy, bool bold) const;
};

