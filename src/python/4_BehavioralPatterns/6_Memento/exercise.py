from unittest import *
from copy import deepcopy


class Token:
    def __init__(self, value=0):
        self.value = value


class Memento(list):
    pass


class TokenMachine:
    def __init__(self):
        self.tokens = []

    def add_token_value(self, value):
        return self.add_token(Token(value))

    def add_token(self, token):
        self.tokens.append(token)
        return Memento(deepcopy(self.tokens))

    def revert(self, memento):
        self.tokens = [Token(x.value) for x in memento]


class Evaluate(TestCase):
    def test_single_token(self):
        tm = TokenMachine()
        m = tm.add_token_value(123)
        tm.add_token_value(456)
        tm.revert(m)
        self.assertEqual(1, len(tm.tokens),
                         msg='We expect exactly 1 token')
        self.assertEqual(123, tm.tokens[0].value,
                         msg='The first token\'s value should be 123')

    def test_two_tokens(self):
        tm = TokenMachine()
        tm.add_token_value(1)
        m = tm.add_token_value(2)
        tm.add_token_value(3)
        tm.revert(m)
        self.assertEqual(2, len(tm.tokens), msg='We should have exactly 2 tokens')
        self.assertEqual(1, tm.tokens[0].value,
                         msg='First token should have value 1, you got ' + str(tm.tokens[0].value))
        self.assertEqual(2, tm.tokens[1].value, msg='Second token should have the value 2')

    # this one is deliberately tricky
    def test_fiddled_token(self):
        tm = TokenMachine()

        token = Token(111)
        print('Made a token with value 111 and kept a reference')

        tm.add_token(token)
        print('Added this token to the list')

        m = tm.add_token_value(222)
        print('Added token 222 and kept a memento')

        print('Changed 111 token\'s value to 333... pay attention!')
        token.value = 333

        tm.revert(m)

        self.assertEqual(2, len(tm.tokens),
                         'At this point, token machine should have exactly 2 tokens, you have ' + str(len(tm.tokens)))

        self.assertEqual(111, tm.tokens[0].value,
                         'You got the tokens[0] value wrong here. Hint: did you init the memento by value?')
