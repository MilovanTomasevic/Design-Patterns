import re
import unittest
from enum import Enum

def megasplit(pattern, string):
    splits = list((m.start(), m.end()) for m in re.finditer(pattern, string))
    starts = [0] + [i[1] for i in splits]
    ends = [i[0] for i in splits] + [len(string)]
    return [string[start:end] for start, end in zip(starts, ends)]

class ExpressionProcessor:
    class NextOp(Enum):
        PLUS = 1
        MINUS = 2

    def __init__(self):
        self.variables = {}

    def calculate(self, expression):
        current = 0
        next_op = None

        # doesn't work in python 3.5
        # parts = re.split('(?<=[+-])', expression)
        parts = megasplit('(?<=[+-])', expression)

        for part in parts:
            noop = re.split('[\+\-]', part)
            first = noop[0]
            value = 0

            try:
                value = int(first)
            except ValueError:
                if len(first) == 1 and first[0] in self.variables:
                    value = self.variables[first[0]]
                else:
                    return 0

            if not next_op:
                current = value
            elif next_op == self.NextOp.PLUS:
                current += value
            elif next_op == self.NextOp.MINUS:
                current -= value

            if part.endswith('+'):
                next_op = self.NextOp.PLUS
            elif part.endswith('-'):
                next_op = self.NextOp.MINUS

        return current


class FirstTestSuite(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        ep = ExpressionProcessor()
        ep.variables['x'] = 5
        cls.ep = ep

    def test_simple(self):
        self.assertEqual(1, self.ep.calculate('1'))

    def test_addition(self):
        self.assertEqual(3, self.ep.calculate('1+2'))

    def test_addition_with_variable(self):
        self.assertEqual(6, self.ep.calculate('1+x'))

    def test_failure(self):
        self.assertEqual(0, self.ep.calculate('1+xy'))
