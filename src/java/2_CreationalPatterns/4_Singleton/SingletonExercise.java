package com.activemesa.creational.singleton.exercise;

import java.util.function.Supplier;

class SingletonTester
{
  public static boolean isSingleton(Supplier<Object> func)
  {
    return func.get() == func.get();
  }
}