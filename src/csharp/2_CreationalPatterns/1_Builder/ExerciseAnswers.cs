using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace DotNetDesignPatternDemos.Creational.Builder
{
  namespace Coding.Exercise
  {
    class Field
    {
      public string Type, Name;

      public override string ToString()
      {
        return $"public {Type} {Name}";
      }
    }

    class Class
    {
      public string Name;
      public List<Field> Fields = new List<Field>();

      public Class()
      {

      }

      public override string ToString()
      {
        var sb = new StringBuilder();
        sb.AppendLine($"public class {Name}").AppendLine("{");
        foreach (var f in Fields)
          sb.AppendLine($"  {f};");
        sb.AppendLine("}");
        return sb.ToString();
      }
    }

    public class CodeBuilder
    {
      public CodeBuilder(string rootName)
      {
        theClass.Name = rootName;
      }

      public CodeBuilder AddField(string name, string type)
      {
        theClass.Fields.Add(new Field { Name = name, Type = type });
        return this;
      }

      public override string ToString()
      {
        return theClass.ToString();
      }

      private Class theClass = new Class();
    }
  }

  namespace Coding.Exercise.UnitTests
  {
    [TestFixture]
    public class FirstTestSuite
    {
      private static string Preprocess(string s)
      {
        return s.Trim().Replace("\r\n", "\n");
      }

      [Test]
      public void EmptyTest()
      {
        var cb = new CodeBuilder("Foo");
        Assert.That(Preprocess(cb.ToString()), Is.EqualTo("public class Foo\n{\n}"));
      }

      [Test]
      public void PersonTest()
      {
        var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
        Assert.That(Preprocess(cb.ToString()),
          Is.EqualTo(
            @"public class Person
{
  public string Name;
  public int Age;
}"
          ));
      }
    }
  }
}