package com.activemesa.creational.prototype.copyserial;

import org.apache.commons.lang3.SerializationUtils;

import java.io.Serializable;

// some libraries use reflection (no need for Serializable)
class Foo implements Serializable
{
  public int stuff;
  public String whatever;

  public Foo(int stuff, String whatever)
  {
    this.stuff = stuff;
    this.whatever = whatever;
  }

  @Override
  public String toString()
  {
    return "Foo{" +
      "stuff=" + stuff +
      ", whatever='" + whatever + '\'' +
      '}';
  }
}

class CopyThroughSerializationDemo
{
  public static void main(String[] args)
  {
    Foo foo = new Foo(42, "life");
    // use apache commons!
    Foo foo2 = SerializationUtils.roundtrip(foo);

    foo2.whatever = "xyz";

    System.out.println(foo);
    System.out.println(foo2);
  }
}
