class Property
{
  constructor(value, name='')
  {
    this._value = value;
    this.name = name;
  }

  get value() { return this._value; }
  set value(newValue)
  {
    if (this._value === newValue)
      return;
    console.log(`Assigning ${newValue} to ${this.name}`);
    this._value = newValue;
  }
}

class Creature
{
  constructor()
  {
    this._agility = new Property(10, 'agility');
  }

  get agility() { return this._agility.value; }
  set agility(value) {
    this._agility.value = value;
  }
}

let c = new Creature();
c.agility = 12;
c.agility = 13;