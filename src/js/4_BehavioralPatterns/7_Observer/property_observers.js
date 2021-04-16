class Event
{
  constructor()
  {
    this.handlers = new Map();
    this.count = 0;
  }

  subscribe(handler)
  {
    this.handlers.set(++this.count, handler);
    return this.count;
  }

  unsubscribe(idx)
  {
    this.handlers.delete(idx);
  }

  // 1) who fired the event?
  // 2) additional data (event args)
  fire(sender, args)
  {
    this.handlers.forEach(
      (v, k) => v(sender, args)
    );
  }
}

class PropertyChangedArgs
{
  constructor(name, newValue)
  {
    this.name = name;
    this.newValue = newValue;
  }
}

class Person
{
  constructor(age)
  {
    this._age = age;
    this.propertyChanged = new Event();
  }

  get age() { return this._age; }

  set age(value)
  {
    if (!value || this._age === value)
      return;
    this._age = value;
    this.propertyChanged.fire(
      this,
      new PropertyChangedArgs('age', value)
    );
  }
}

class RegistrationChecker
{
  constructor(person)
  {
    this.person = person;
    this.token = person.propertyChanged.subscribe(
      this.age_changed.bind(this)
    );
  }

  age_changed(sender, args)
  {
    if (sender === this.person && args.name === 'age')
    {
      if (args.newValue < 13)
      {
        console.log(`Sorry, you are still to young`);
      } else {
        console.log(`Okay, you can register`);
        sender.propertyChanged.unsubscribe(this.token);
      }
    }
  }
}

let person = new Person('John');
let checker = new RegistrationChecker(person);
for (let i = 10; i < 20; ++i)
{
  console.log(`Changing age to ${i}`);
  person.age = i; //
}