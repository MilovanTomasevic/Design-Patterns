from enum import Enum, auto


class State(Enum):
    LOCKED = auto()
    FAILED = auto()
    UNLOCKED = auto()


if __name__ == '__main__':
    code = '1234'
    state = State.LOCKED
    entry = ''

    while True:
        if state == State.LOCKED:
            entry += input(entry)

            if entry == code:
                state = State.UNLOCKED

            if not code.startswith(entry):
                # the code is wrong
                state = State.FAILED
        elif state == State.FAILED:
            print('\nFAILED')
            entry = ''
            state = State.LOCKED
        elif state == State.UNLOCKED:
            print('\nUNLOCKED')
            break
