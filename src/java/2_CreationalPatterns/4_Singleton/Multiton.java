package com.activemesa.creational.singleton.multiton;

import java.util.HashMap;

enum Subsystem
{
  PRIMARY,
  AUXILIARY,
  FALLBACK
}

class Printer
{
  private static int instanceCount = 0;

  private Printer()
  {
    instanceCount++;
    System.out.println("A total of " +
      instanceCount + " instances created so far.");
  }

  private static HashMap<Subsystem, Printer>
    instances = new HashMap<>();

  public static Printer get(Subsystem ss)
  {
    if (instances.containsKey(ss))
      return instances.get(ss);

    Printer instance = new Printer();
    instances.put(ss, instance);
    return instance;
  }
}

public class Multiton {
  public static void main(String[] args) {
    Printer main = Printer.get(Subsystem.PRIMARY);
    Printer aux = Printer.get(Subsystem.AUXILIARY);
    Printer aux2 = Printer.get(Subsystem.AUXILIARY);
  }
}
