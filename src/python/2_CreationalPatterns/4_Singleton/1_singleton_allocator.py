import random

class Database:
    initialized = False

    def __init__(self):
        # self.id = random.randint(1,101)
        # print('Generated an id of ', self.id)
        # print('Loading database from file')
        pass

    _instance = None

    def __new__(cls, *args, **kwargs):
        if not cls._instance:
            cls._instance = super(Database, cls)\
                .__new__(cls, *args, **kwargs)

        return cls._instance


database = Database()

if __name__ == '__main__':
    d1 = Database()
    d2 = Database()

    print(d1.id, d2.id)
    print(d1 == d2)
    print(database == d1)