package com.activemesa.creational.prototype;

import java.util.Arrays;

// Cloneable is a marker interface
class Address implements Cloneable {
  public String streetName;
  public int houseNumber;

  public Address(String streetName, int houseNumber)
  {
    this.streetName = streetName;
    this.houseNumber = houseNumber;
  }

  @Override
  public String toString()
  {
    return "Address{" +
      "streetName='" + streetName + '\'' +
      ", houseNumber=" + houseNumber +
      '}';
  }

  // base class clone() is protected
  @Override
  public Object clone() throws CloneNotSupportedException
  {
    return new Address(streetName, houseNumber);
  }
}

class Person implements Cloneable
{
  public String [] names;
  public Address address;

  public Person(String[] names, Address address)
  {
    this.names = names;
    this.address = address;
  }

  @Override
  public String toString()
  {
    return "Person{" +
      "names=" + Arrays.toString(names) +
      ", address=" + address +
      '}';
  }

  @Override
  public Object clone() throws CloneNotSupportedException
  {
    return new Person(
      // clone() creates a shallow copy!
      /*names */ names.clone(),

      // fixes address but not names
      /*address */ // (Address) address.clone()
      address instanceof Cloneable ? (Address) address.clone() : address
    );
  }
}

class CloneableDemo
{
  public static void main(String[] args)
    throws CloneNotSupportedException
  {
    Person john = new Person(new String[]{"John", "Smith"},
      new Address("London Road", 123));

    // shallow copy, not good:
    //Person jane = john;

    // jane is the girl next door
    Person jane = (Person) john.clone();
    jane.names[0] = "Jane"; // clone is (originally) shallow copy
    jane.address.houseNumber = 124; // oops, also changed john

    System.out.println(john);
    System.out.println(jane);
  }
}
