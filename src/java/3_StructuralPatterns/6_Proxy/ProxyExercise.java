package com.activemesa.structural.proxy.exercise;

class Person
{
  private int age;

  public Person(int age)
  {
    this.age = age;
  }

  public int getAge()
  {
    return age;
  }

  public void setAge(int age)
  {
    this.age = age;
  }

  public String drink() { return "drinking"; }
  public String drive() { return "driving"; }
  public String drinkAndDrive() { return "driving while drunk"; }
}

class ResponsiblePerson
{
  private Person person;

  public ResponsiblePerson(Person person)
  {
    this.person = person;
  }

  public void setAge(int value) { person.setAge(value); }
  public int getAge() { return person.getAge(); }

  public String drink()
  {
    return getAge() >= 18 ? person.drink() : "too young";
  }

  public String drive()
  {
    return getAge() >= 16 ? person.drive() : "too young";
  }

  public String drinkAndDrive()
  {
    return "dead";
  }
}