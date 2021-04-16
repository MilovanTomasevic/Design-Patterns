import unittest
from abc import ABC
from enum import Enum

# creature removal (unsubscription) ignored in this exercise solution

class Creature(ABC):
    def __init__(self, game, attack, defense):
        self.initial_defense = defense
        self.initial_attack = attack
        self.game = game

    @property
    def attack(self): pass

    @property
    def defense(self): pass

    def query(self, source, query): pass


class WhatToQuery(Enum):
    ATTACK = 1
    DEFENSE = 2


class Goblin(Creature):

    def __init__(self, game, attack=1, defense=1):
        super().__init__(game, attack, defense)

    @property
    def attack(self):
        q = Query(self.initial_attack, WhatToQuery.ATTACK)
        for c in self.game.creatures:
            c.query(self, q)
        return q.value

    @property
    def defense(self):
        q = Query(self.initial_defense, WhatToQuery.DEFENSE)
        for c in self.game.creatures:
            c.query(self, q)
        return q.value

    def query(self, source, query):
        if self != source and query.what_to_query == WhatToQuery.DEFENSE:
            query.value += 1


class GoblinKing(Goblin):

    def __init__(self, game):
        super().__init__(game, 3, 3)

    def query(self, source, query):
        if self != source and query.what_to_query == WhatToQuery.ATTACK:
            query.value += 1
        else:
            super().query(source, query)


class Query:
    def __init__(self, initial_value, what_to_query):
        self.what_to_query = what_to_query
        self.value = initial_value

class Game:
    def __init__(self):
        self.creatures = []


class FirstTestSuite(unittest.TestCase):
    def test(self):
        game = Game()
        goblin = Goblin(game)
        game.creatures.append(goblin)

        self.assertEqual(1, goblin.attack)
        self.assertEqual(1, goblin.defense)

        goblin2 = Goblin(game)
        game.creatures.append(goblin2)

        self.assertEqual(1, goblin.attack)
        self.assertEqual(2, goblin.defense)

        goblin3 = GoblinKing(game)
        game.creatures.append(goblin3)

        self.assertEqual(2, goblin.attack)
        self.assertEqual(3, goblin.defense)