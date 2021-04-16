using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DesignPatterns
{
  public enum OutputFormat
  {
    Markdown,
    Html
  }

  public interface IListStrategy
  {
    void Start(StringBuilder sb);
    void End(StringBuilder sb);
    void AddListItem(StringBuilder sb, string item);
  }

  public class MarkdownListStrategy : IListStrategy
  {
    public void Start(StringBuilder sb)
    {
      // markdown doesn't require a list preamble
    }

    public void End(StringBuilder sb)
    {
      
    }

    public void AddListItem(StringBuilder sb, string item)
    {
      sb.AppendLine($" * {item}");
    }
  }

  public class HtmlListStrategy : IListStrategy
  {
    public void Start(StringBuilder sb)
    {
      sb.AppendLine("<ul>");
    }

    public void End(StringBuilder sb)
    {
      sb.AppendLine("</ul>");
    }

    public void AddListItem(StringBuilder sb, string item)
    {
      sb.AppendLine($"  <li>{item}</li>");
    }
  }

  // a.k.a. policy
  public class TextProcessor<LS> where LS : IListStrategy, new()
  {
    private StringBuilder sb = new StringBuilder();
    private IListStrategy listStrategy = new LS();

    public void AppendList(IEnumerable<string> items)
    {
      listStrategy.Start(sb);
      foreach (var item in items)
        listStrategy.AddListItem(sb, item);
      listStrategy.End(sb);
    }

    public override string ToString()
    {
      return sb.ToString();
    }
  }

  class Demo
  { 
    static void Main(string[] args)
    {
      var tp = new TextProcessor<MarkdownListStrategy>();
      tp.AppendList(new []{"foo", "bar", "baz"});
      WriteLine(tp);

      var tp2 = new TextProcessor<HtmlListStrategy>();
      tp2.AppendList(new[] { "foo", "bar", "baz" });
      WriteLine(tp2);
    }
  }
}