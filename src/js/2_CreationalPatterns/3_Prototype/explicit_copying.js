class Address
{
  constructor(streetAddress, city, country) {
    this.streetAddress = streetAddress;
    this.city = city;
    this.country = country;
  }

  deepCopy()
  {
    return new Address(
      this.streetAddress,
      this.city,
      this.country
    );
  }

  toString()
  {
    return `Address: ${this.streetAddress}, ` +
      `${this.city}, ${this.country}`;
  }
}

class Person
{
  constructor(name, address)
  {
    this.name = name;
    this.address = address; //!
  }

  deepCopy()
  {
    return new Person(
      this.name,
      this.address.deepCopy() // needs to be recursive
    );
  }

  toString()
  {
    return `${this.name} lives at ${this.address}`;
  }
}

let john = new Person('John',
  new Address('123 London Road', 'London', 'UK'));

let jane = john.deepCopy();

jane.name = 'Jane';
jane.address.streetAddress = '321 Angel St'; // oops

console.log(john.toString()); // oops, john is called 'jane'
console.log(jane.toString());