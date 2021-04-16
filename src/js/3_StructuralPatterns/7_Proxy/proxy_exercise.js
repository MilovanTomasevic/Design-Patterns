class Person
{
  constructor(age=0)
  {
    this.age = age;
  }

  drink() { return 'drinking'; }
  drive() { return 'driving'; }
  drinkAndDrive() { return 'driving while drunk'; }
}

class ResponsiblePerson
{
  constructor(person)
  {
    this.person = person;
  }

  get age()
  {
    return this.person.age;
  }

  set age(value)
  {
    this.person.age = value;
  }

  drink()
  {
    if (this.age >= 18)
      return this.person.drink();
    return 'too young';
  }

  drive()
  {
    if (this.age >= 16)
      return this.person.drive();
    return 'too young';
  }

  // don't do it, folks!
  drinkAndDrive()
  {
    return 'dead';
  }
}

describe('proxy', function()
{
  it('does its proxy thing', function()
  {
    let p = new Person(10);
    let rp = new ResponsiblePerson(p);

    expect(rp.drive()).toEqual('too young');
    expect(rp.drink()).toEqual('too young');
    expect(rp.drinkAndDrive()).toEqual('dead');

    rp.age = 20;

    expect(rp.drive()).toEqual('driving');
    expect(rp.drink()).toEqual('drinking');
    expect(rp.drinkAndDrive()).toEqual('dead');
  });
});