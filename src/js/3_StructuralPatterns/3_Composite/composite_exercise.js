class SingleValue
{
  constructor(value)
  {
    this.value = value;
  }

  [Symbol.iterator]()
  {
    let returned = false;
    return {
      next: () => ({
        value: this.value,
        done: returned++
      })
    }
  }
}

class ManyValues extends Array {}

let sum = function(containers)
{
  let result = 0;
  for (let c of containers)
    for (let i of c)
      result += i;
  return result;
};

describe('composite', function()
{
  it('should sum up different objects', function()
  {
    let singleValue = new SingleValue(11);
    let otherValues = new ManyValues();
    otherValues.push(22);
    otherValues.push(33);
    expect(sum([singleValue, otherValues])).toEqual(66);
  });
});