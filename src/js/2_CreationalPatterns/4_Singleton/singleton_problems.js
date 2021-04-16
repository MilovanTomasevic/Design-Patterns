const fs = require('fs');
const path = require('path');

class MyDatabase
{
  constructor()
  {
    const instance = this.constructor.instance;
    if (instance) {
      return instance;
    }

    this.constructor.instance = this;


    console.log(`Initializing database`);
    this.capitals = {};

    let lines = fs.readFileSync(
      path.join(__dirname, 'capitals.txt')
    ).toString().split('\r\n');

    for (let i = 0; i < lines.length/2; ++i)
    {
      this.capitals[lines[2*i]] = parseInt(lines[2*i+1]);
    }
  }

  getPopulation(city)
  {
    // possible error handling here
    return this.capitals[city];
  }
}

// ↑↑↑ low-level module

// ↓↓↓ high-level module

class SingletonRecordFinder
{
  totalPopulation(cities)
  {
    return cities.map(
      city => new MyDatabase().getPopulation(city)
    ).reduce((x,y) => x+y);
  }
}

class ConfigurableRecordFinder
{
  constructor(database)
  {
    this.database = database;
  }

  totalPopulation(cities)
  {
    return cities.map(
      city => this.database.getPopulation(city)
    ).reduce((x,y) => x+y);
  }
}

class DummyDatabase
{
  constructor()
  {
    this.capitals = {
      'alpha': 1,
      'beta': 2,
      'gamma': 3
    };
  }

  getPopulation(city)
  {
    // possible error handling here
    return this.capitals[city];
  }
}

describe('singleton database', function()
{
  it('is a singleton', function()
  {
    const db1 = new MyDatabase();
    const db2 = new MyDatabase();
    expect(db1).toBe(db2);
  });

  it('calculates total population', function()
  {
    let rf = new SingletonRecordFinder();
    let cities = ['Seoul', 'Mexico City'];
    let tp = rf.totalPopulation(cities);
    expect(tp).toEqual(17400000+17500000);
  });

  it('calculates total population better', function()
  {
    let db = new DummyDatabase();
    let rf = new ConfigurableRecordFinder(db);
    expect(rf.totalPopulation(['alpha', 'gamma'])).toEqual(4);
  });
});