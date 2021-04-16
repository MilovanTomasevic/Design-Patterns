import unittest


class CombinationLock:
    def __init__(self, combination):
        self.combination = combination
        self.reset()

    def reset(self):
        self.status = 'LOCKED'
        self.digits_entered = 0
        self.failed = False

    def enter_digit(self, digit):
        if self.status == 'LOCKED':
            self.status = ''
        self.status += str(digit)
        if self.combination[self.digits_entered] != digit:
            self.failed = True
        self.digits_entered += 1

        if self.digits_entered == len(self.combination):
            self.status = 'ERROR' if self.failed else 'OPEN'


class FirstTestSuite(unittest.TestCase):
    def test_success(self):
        cl = CombinationLock([1, 2, 3, 4, 5])
        self.assertEqual('LOCKED', cl.status)
        cl.enter_digit(1)
        self.assertEqual('1', cl.status)
        cl.enter_digit(2)
        self.assertEqual('12', cl.status)
        cl.enter_digit(3)
        self.assertEqual('123', cl.status)
        cl.enter_digit(4)
        self.assertEqual('1234', cl.status)
        cl.enter_digit(5)
        self.assertEqual('OPEN', cl.status)

    def test_failure(self):
        cl = CombinationLock([1, 2, 3])
        self.assertEqual('LOCKED', cl.status)
        cl.enter_digit(1)
        self.assertEqual('1', cl.status)
        cl.enter_digit(2)
        self.assertEqual('12', cl.status)
        cl.enter_digit(5)
        self.assertEqual('ERROR', cl.status)
