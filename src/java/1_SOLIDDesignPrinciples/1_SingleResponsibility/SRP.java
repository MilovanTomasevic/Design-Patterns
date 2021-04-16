package com.activemesa.solid.srp;

import java.io.*;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

import static java.lang.System.out;

class Journal
{
  private final List<String> entries = new ArrayList<>();

  private static int count = 0;

  public void addEntry(String text)
  {
    entries.add("" + (++count) + ": " + text);
  }

  public void removeEntry(int index)
  {
    entries.remove(index);
  }

  @Override
  public String toString() {
    return String.join(System.lineSeparator(), entries);
  }

  // here we break SRP
  public void save(String filename) throws Exception
  {
    try (PrintStream out = new PrintStream(filename))
    {
      out.println(toString());
    }
  }

  public void load(String filename) {}
  public void load(URL url) {}
}

// handles the responsibility of persisting objects
class Persistence
{
  public void saveToFile(Journal journal, 
    String filename, boolean overwrite) throws Exception
  {
    if (overwrite || new File(filename).exists())
      try (PrintStream out = new PrintStream(filename)) {
        out.println(journal.toString());
      }
  }

  public void load(Journal journal, String filename) {}
  public void load(Journal journal, URL url) {}
}

class SRPDemo
{
  public static void main(String[] args) throws Exception
  {
    Journal j = new Journal();
    j.addEntry("I cried today");
    j.addEntry("I ate a bug");
    System.out.println(j);

    Persistence p = new Persistence();
    String filename = "c:\\temp\\journal.txt";
    p.saveToFile(j, filename, true);

    // windows!
    Runtime.getRuntime().exec("notepad.exe " + filename);
  }
}