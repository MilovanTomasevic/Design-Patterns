using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDesignPatternDemos.Structural.Decorator
{
  public class Bird
  {
    public void Fly()
    {
      
    }
  }

  public class Lizard
  {
    public void Crawl()
    {
      
    }
  }

  public class Dragon // no multiple inheritance
  {
    private Bird bird;
    private Lizard lizard;

    public Dragon(Bird bird, Lizard lizard)
    {
      this.bird = bird ?? throw new ArgumentNullException(paramName: nameof(bird));
      this.lizard = lizard ?? throw new ArgumentNullException(paramName: nameof(lizard));
    }

    public void Crawl()
    {
      lizard.Crawl();
    }

    public void Fly()
    {
      bird.Fly();
    }
  }
}
