using System;
using Autofac;

namespace AutofacDemos
{
  public interface IReportingService
  {
    void Report();
  }

  public class ReportingService : IReportingService
  {
    public void Report()
    {
      Console.WriteLine("Here is your report");
    }
  }

  public class ReportingServiceWithLogging : IReportingService
  {
    private IReportingService decorated;

    public ReportingServiceWithLogging(IReportingService decorated)
    {
      if (decorated == null)
      {
        throw new ArgumentNullException(paramName: nameof(decorated));
      }
      this.decorated = decorated;
    }

    public void Report()
    {
      Console.WriteLine("Commencing log...");
      decorated.Report();
      Console.WriteLine("Ending log...");
    }
  }

  public class Decorators
  {
    static void Main_(string[] args)
    {
      var b = new ContainerBuilder();
      b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
      b.RegisterDecorator<IReportingService>(
          (context, service) => new ReportingServiceWithLogging(service),
        "reporting");

      // open generic decorators also supported
      // b.RegisterGenericDecorator()

      using (var c = b.Build())
      {
        var r = c.Resolve<IReportingService>();
        r.Report();
      }
    }
  }
}