class Car
{
  drive()
  {
    console.log('Car being driven');
  }
}

class CarProxy
{
  constructor(driver)
  {
    this.driver = driver;
    this._car = new Car();
  }

  drive()
  {
    if (this.driver.age >= 16)
      this._car.drive();
    else
      console.log('Driver too young');
  }
}

class Driver
{
  constructor(age)
  {
    this.age = age;
  }
}

let car = new Car();
car.drive();

let car2 = new CarProxy(new Driver(12)); // try 22
car2.drive();

