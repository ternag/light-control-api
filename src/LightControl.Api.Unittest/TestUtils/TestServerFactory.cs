using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace LightControl.Api.UnitTest.TestUtils;

public class TestServerFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly ITestOutputHelper _outputHelper;

    public TestServerFactory(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddXUnit(_outputHelper); // Route logging to xUnit output helper
        });
        builder.UseEnvironment("Development");
    }
}