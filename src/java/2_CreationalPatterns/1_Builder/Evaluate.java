package com.activemesa.creational.builder.exercise;

import org.junit.Assert;
import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class Evaluate
{
  private String preprocess(String text)
  {
    return text.replace("\r\n", "\n").trim();
  }

  @Test
  public void emptyTest() {
    CodeBuilder cb = new CodeBuilder("Foo");
    assertEquals("public class Foo\n{\n}",
      preprocess(cb.toString()));
  }

  @Test
  public void personTest()
  {
    CodeBuilder cb = new CodeBuilder("Person")
      .addField("name", "String")
      .addField("age", "int");
    assertEquals("public class Person\n{\n" +
      "  public String name;\n" +
      "  public int age;\n}",
      preprocess(cb.toString()));
  }
}
