package com.activemesa.creational.factories;

import org.javatuples.Pair;
import org.reflections.Reflections;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.lang.reflect.Type;
import java.util.*;

interface IHotDrink
{
  void consume();
}

class Tea implements IHotDrink
{
  @Override
  public void consume()
  {
    System.out.println("This tea is nice but I'd prefer it with milk.");
  }
}

class Coffee implements IHotDrink
{
  @Override
  public void consume()
  {
    System.out.println("This coffee is delicious");
  }
}

interface IHotDrinkFactory
{
  IHotDrink prepare(int amount);
}

class TeaFactory implements IHotDrinkFactory
{
  @Override
  public IHotDrink prepare(int amount)
  {
    System.out.println(
      "Put in tea bag, boil water, pour "
      + amount + "ml, add lemon, enjoy!"
    );
    return new Tea();
  }
}

class CoffeeFactory implements IHotDrinkFactory
{

  @Override
  public IHotDrink prepare(int amount)
  {
    System.out.println(
      "Grind some beans, boil water, pour "
      + amount + " ml, add cream and sugar, enjoy!"
    );
    return new Coffee();
  }
}

class HotDrinkMachine
{
  public enum AvailableDrink
  {
    COFFEE, TEA
  }

  private Map<AvailableDrink, IHotDrinkFactory> factories =
    new HashMap<>();

  private List<Pair<String, IHotDrinkFactory>> namedFactories =
    new ArrayList<>();

  public HotDrinkMachine() throws Exception
  {
    // option 1: use an enum
    for (AvailableDrink drink : AvailableDrink.values())
    {
      String s = drink.toString();
      String factoryName = "" + Character.toUpperCase(s.charAt(0)) + s.substring(1).toLowerCase();
      Class<?> factory = Class.forName("com.activemesa.creational.factories." + factoryName + "Factory");
      factories.put(drink, (IHotDrinkFactory) factory.getDeclaredConstructor().newInstance());
    }

    // option 2: find all implementors of IHotDrinkFactory
    Set<Class<? extends IHotDrinkFactory>> types = 
      new Reflections("com.activemesa.creational.factories") // ""
      .getSubTypesOf(IHotDrinkFactory.class);
    for (Class<? extends IHotDrinkFactory> type : types)
    {
      namedFactories.add(new Pair<>(
        type.getSimpleName().replace("Factory", ""),
        type.getDeclaredConstructor().newInstance()
      ));
    }
  }

  public IHotDrink makeDrink() throws IOException
  {
    System.out.println("Available drinks");
    for (int index = 0; index < namedFactories.size(); ++index)
    {
      Pair<String, IHotDrinkFactory> item = namedFactories.get(index);
      System.out.println("" + index + ": " + item.getValue0());
    }

    BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
    while (true)
    {
      String s;
      int i, amount;
      if ((s = reader.readLine()) != null
        && (i = Integer.parseInt(s)) >= 0
        && i < namedFactories.size())
      {
        System.out.println("Specify amount: ");
        s = reader.readLine();
        if (s != null
          && (amount = Integer.parseInt(s)) > 0)
        {
          return namedFactories.get(i).getValue1().prepare(amount);
        }
      }
      System.out.println("Incorrect input, try again.");
    }
  }

  public IHotDrink makeDrink(AvailableDrink drink, int amount)
  {
    return ((IHotDrinkFactory)factories.get(drink)).prepare(amount);
  }
}

class AbstractFactoryDemo
{
  public static void main(String[] args) throws Exception
  {
    HotDrinkMachine machine = new HotDrinkMachine();
    IHotDrink tea = machine.makeDrink(HotDrinkMachine.AvailableDrink.TEA, 200);
    tea.consume();

    // interactive
    IHotDrink drink = machine.makeDrink();
    drink.consume();
  }
}
