using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace LightControl.Api.UnitTest.TestUtils
{
  public class TestServerFactory
  {
    private readonly ITestOutputHelper _outputHelper;

    public TestServerFactory(ITestOutputHelper outputHelper)
    {
      _outputHelper = outputHelper;
    }

    public static TestServer Create(ITestOutputHelper outputHelper, LogLevel logLevel = LogLevel.Information)
    {
      var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.UnitTest.json", optional: false, reloadOnChange: true)
        .Build();
      
      var server = new TestServer(new WebHostBuilder()
        .UseEnvironment("Development")
        .UseStartup<Startup>()
        .UseConfiguration(configuration)
        .ConfigureLogging(logging =>
        {
          logging.ClearProviders();
          logging.AddXUnit(outputHelper); // Route logging to xUnit output helper
          logging.SetMinimumLevel(logLevel); // comment out to use default log level => info 
        }));
      return server;
    }
  }
}