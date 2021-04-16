class Stuff
{
  constructor()
  {
    this.a = 11;
    this.b = 22;
  }

  // default iterator
  [Symbol.iterator]()
  {
    let i = 0;
    let self = this;
    return {
      next: function()
      {
        return {
          done: i > 1,
          value: self[i++ === 0 ? 'a' : 'b']
        };
      }
    }
  }

  get backwards()
  {
    let i = 0;
    let self = this;
    return {
      next: function()
      {
        return {
          done: i > 1,
          value: self[i++ === 0 ? 'b' : 'a']
        };
      },
      // make iterator iterable
      [Symbol.iterator]: function() { return this; }
    }
  }
}

let values = [100, 200, 300];
for (let i in values)
{
  console.log(`Element at pos ${i} is ${values[i]}`);
}

for (let v of values)
{
  console.log(`Value is ${v}`);
}

let stuff = new Stuff();
for (let item of stuff)
  console.log(`${item}`);

for (let item of stuff.backwards)
  console.log(`${item}`);