class Event(list):
    def __call__(self, *args, **kwargs):
        for item in self:
            item(*args, **kwargs)


class Person:
    def __init__(self, name, address):
        self.name = name
        self.address = address
        self.falls_ill = Event()

    def catch_a_cold(self):
        self.falls_ill(self.name, self.address)


def call_doctor(name, address):
    print(f'A doctor has been called to {address}')

if __name__ == '__main__':
    person = Person('Sherlock', '221B Baker St')

    person.falls_ill.append(lambda name, addr: print(f'{name} is ill'))

    person.falls_ill.append(call_doctor)

    person.catch_a_cold()

    # and you can remove subscriptions too
    person.falls_ill.remove(call_doctor)
    person.catch_a_cold()