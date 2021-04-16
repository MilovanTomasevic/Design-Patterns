class Person
{
  constructor(id, name)
  {
    this.id = id;
    this.name = name;
  }
}

class PersonFactory
{
  createPerson(name)
  {
    return new Person(
      PersonFactory.id++,
      name
    );
  }
}
PersonFactory.id = 0;

describe('factory', function()
{
  it('exercise', function()
  {
    let pf = new PersonFactory();

    let p1 = pf.createPerson('Chris');
    expect(p1.name).toEqual('Chris');
    expect(p1.id).toEqual(0);

    let p2 = pf.createPerson('Sarah');
    expect(p2.id).toEqual(1);
  });
});