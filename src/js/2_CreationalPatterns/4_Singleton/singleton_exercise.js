class SingletonTester
{
  static isSingleton(generator)
  {
    let obj1 = generator();
    let obj2 = generator();
    console.log(obj1.toString());
    console.log(obj2.toString());
    console.log(`Returning ${obj1 === obj2}`);
    return obj1 === obj2;
  }
}

describe('singleton', function()
{
  it('test with a real singleton', function()
  {
    const item = [1, 2, 3];

    expect(SingletonTester.isSingleton(() => item))
      .toBe(true);
  });

  it('test with a non-singleton', function()
  {
    let result = SingletonTester.isSingleton(
      () => [ Math.random() ]
      );
    expect(result).toBe(false);
  });
});