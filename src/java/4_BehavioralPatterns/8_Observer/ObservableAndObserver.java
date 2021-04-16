package com.activemesa.behavioral.observer.oando;

import java.util.ArrayList;
import java.util.List;

class PropertyChangedEventArgs<T>
{
  public T source;
  public String propertyName;
  public Object newValue;

  public PropertyChangedEventArgs(T source, String propertyName,
                                  Object newValue)
  {
    this.source = source;
    this.propertyName = propertyName;
    this.newValue = newValue;
  }
}

// observes objects of type T
interface Observer<T>
{
  void handle(PropertyChangedEventArgs<T> args);
}

// can be observed
class Observable<T>
{
  private List<Observer<T>> observers = new ArrayList<>();

  public void subscribe(Observer<T> observer)
  {
    observers.add(observer);
  }

  protected void propertyChanged(T source,
                                 String propertyName,
                                 Object newValue)
  {
    for (Observer<T> o : observers)
      o.handle(new PropertyChangedEventArgs<T>(
        source, propertyName, newValue
      ));
  }
}

class Person extends Observable<Person>
{
  private int age;

  public int getAge()
  {
    return age;
  }
  public void setAge(int age)
  {
    if (this.age == age) return;
    this.age = age;
    propertyChanged(this, "age", age);
  }
}

class ObserverInfrastructureDemo implements Observer<Person>
  // Observer<Foo>
{
  public ObserverInfrastructureDemo()
  {
    Person person = new Person();
    person.subscribe(this);
    for (int i = 20; i < 24; ++i)
      person.setAge(i);
  }

  public static void main(String [] args)
  {
    new ObserverInfrastructureDemo();
  }

  @Override
  public void handle(PropertyChangedEventArgs<Person> args)
  {
    System.out.println("Person's " + args.propertyName
      + " has been changed to " + args.newValue);
  }
}
