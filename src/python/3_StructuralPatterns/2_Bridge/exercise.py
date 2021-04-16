# class Shape:
#     def __init__(self):
#         self.name = None
#
#
# class Triangle(Shape):
#     def __init__(self):
#         super().__init__()
#         self.name = 'Triangle'
#
#
# class Square(Shape):
#     def __init__(self):
#         super().__init__()
#         self.name = 'Square'
#
#
# class VectorSquare(Square):
#     def __str__(self):
#         return f'Drawing {self.name} as lines'
#
#
# class RasterSquare(Square):
#     def __str__(self):
#         return f'Drawing {self.name} as pixels'

# imagine VectorTriangle and RasterTriangle are here too
import unittest
from abc import ABC


class Renderer(ABC):
    @property
    def what_to_render_as(self):
        return None


class Shape(ABC):
    def __init__(self, renderer, name):
        self.renderer = renderer
        self.name = name

    def __str__(self):
        return 'Drawing %s as %s' % (self.name, self.renderer.what_to_render_as)


class Triangle(Shape):
    def __init__(self, renderer):
        super().__init__(renderer, 'Triangle')


class Square(Shape):
    def __init__(self, renderer):
        super().__init__(renderer, 'Square')


class RasterRenderer(Renderer):
    @property
    def what_to_render_as(self):
        return 'pixels'


class VectorRenderer(Renderer):
    @property
    def what_to_render_as(self):
        return 'lines'


class Evaluate(unittest.TestCase):
    def test_square_vector(self):
        sq = Square(VectorRenderer())
        self.assertEqual(str(sq), 'Drawing Square as lines')

    def test_pixel_triangle(self):
        tr = Triangle(RasterRenderer())
        self.assertEqual(str(tr), 'Drawing Triangle as pixels')
