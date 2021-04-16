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

  public class TextProcessor
  {
    private StringBuilder sb = new StringBuilder();
    private IListStrategy listStrategy;

    public void SetOutputFormat(OutputFormat format)
    {
      switch (format) {
        case OutputFormat.Markdown:
          listStrategy = new MarkdownListStrategy();
          break;
        case OutputFormat.Html:
          listStrategy = new HtmlListStrategy();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(format), format, null);
      }
    }

    public void AppendList(IEnumerable<string> items)
    {
      listStrategy.Start(sb);
      foreach (var item in items)
        listStrategy.AddListItem(sb, item);
      listStrategy.End(sb);
    }

    public StringBuilder Clear()
    {
      return sb.Clear();
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
      var tp = new TextProcessor();
      tp.SetOutputFormat(OutputFormat.Markdown);
      tp.AppendList(new []{"foo", "bar", "baz"});
      WriteLine(tp);

      tp.Clear();
      tp.SetOutputFormat(OutputFormat.Html);
      tp.AppendList(new[] { "foo", "bar", "baz" });
      WriteLine(tp);
    }
  }
}