package com.activemesa.creational.singleton.singleton;

import java.io.*;

// since Java 1.5
// you cannot inherit from this
enum EnumBasedSingleton
{
  INSTANCE; // hooray

  // enum type already has a private ctor by default,
  // but if you need to initialize things...
  EnumBasedSingleton() {
    value = 42;
  }

  // enums are inherently serializable (but what good is that?)
  // reflection not a problem, guaranteed 1 instance in JVM

  // field values not serialized!
  int value;

  public int getValue()
  {
    return value;
  }
  public void setValue(int value)
  {
    this.value = value;
  }
}

class EnumBasedSingletonDemo
{
  static void saveToFile(EnumBasedSingleton singleton, String filename)
    throws Exception
  {
    try (FileOutputStream fileOut = new FileOutputStream(filename);
         ObjectOutputStream out = new ObjectOutputStream(fileOut))
    {
      out.writeObject(singleton);
    }
  }

  static EnumBasedSingleton readFromFile(String filename)
    throws Exception
  {
    try (FileInputStream fileIn = new FileInputStream(filename);
         ObjectInputStream in = new ObjectInputStream(fileIn) )
    {
      return (EnumBasedSingleton)in.readObject();
    }
  }

  //
  public static void main(String[] args)
    throws Exception
  {
    String filename = "myfile.bin";

    // run again with next 3 lines commented out

//    EnumBasedSingleton singleton = EnumBasedSingleton.INSTANCE;
//    singleton.setValue(111);
//    saveToFile(singleton, filename);

    EnumBasedSingleton singleton2 = readFromFile(filename);
    System.out.println(singleton2.getValue());
  }
}