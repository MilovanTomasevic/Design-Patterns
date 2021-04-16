class CombinationLock
{
  constructor(combination)
  {
    this.combination = combination;
    this.reset();
  }

  reset()
  {
    this.status = 'LOCKED';
    this.digitsEntered = 0;
    this.failed = false;
  }

  enterDigit(digit)
  {
    if (this.status === 'LOCKED')
      this.status = '';
    this.status += digit.toString();
    if (this.combination[this.digitsEntered] !== digit)
    {
      this.failed = true;
    }
    this.digitsEntered++;

    if (this.digitsEntered === this.combination.length)
      this.status = this.failed ? 'ERROR' : 'OPEN';
  }
}

describe('state', function()
{
  it('test success', function()
  {
    let cl = new CombinationLock([1, 2, 3, 4, 5]);
    expect(cl.status).toEqual('LOCKED');
    cl.enterDigit(1);
    expect(cl.status).toEqual('1');
    cl.enterDigit(2);
    expect(cl.status).toEqual('12');
    cl.enterDigit(3);
    expect(cl.status).toEqual('123');
    cl.enterDigit(4);
    expect(cl.status).toEqual('1234');
    cl.enterDigit(5);
    expect(cl.status).toEqual('OPEN');
  });

  it('test failure', function()
  {
    let cl = new CombinationLock([1, 2, 3]);
    expect(cl.status).toEqual('LOCKED');
    cl.enterDigit(1);
    expect(cl.status).toEqual('1');
    cl.enterDigit(2);
    expect(cl.status).toEqual('12');
    cl.enterDigit(5);
    expect(cl.status).toEqual('ERROR');
  });
});