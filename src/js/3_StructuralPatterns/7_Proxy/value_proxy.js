class Percentage
{
  constructor(percent)
  {
    this.percent = percent; // 0 to 100
  }

  toString()
  {
    return `${this.percent}%`;
  }

  valueOf()
  {
    return this.percent/100;
  }
}

let fivePercent = new Percentage(5);
console.log(`${fivePercent} of 50 is ${50*fivePercent}`);