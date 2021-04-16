class Bird
{
  constructor(age=0)
  {
    this.age = age;
  }

  fly()
  {
    return this.age < 10 ? 'flying' : 'too old';
  }
}

class Lizard
{
  constructor(age=0)
  {
    this.age = age;
  }

  crawl()
  {
    return this.age > 1 ? 'crawling' : 'too young';
  }
}

class Dragon
{
  constructor(age=0)
  {
    this._bird = new Bird();
    this._lizard = new Lizard();
    this._age = age;
  }

  set age(value) {
    this._age = this._bird.age
      = this._lizard.age = value;
  }

  get age() { return this._age; }

  fly() { return this._bird.fly(); }
  crawl() { return this._lizard.crawl(); }
}

describe('decorator', function()
{
  it('should let dragon fly or crawl', function()
  {
    let dragon = new Dragon();

    expect(dragon.fly()).toEqual('flying');
    expect(dragon.crawl()).toEqual('too young');

    dragon.age = 20;

    expect(dragon.fly()).toEqual('too old');
    expect(dragon.crawl()).toEqual('crawling');
  });
});