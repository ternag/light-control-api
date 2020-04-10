using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace LightControl.Api.UnitTest
{
  public class HighLevelTests
  {
    private readonly ITestOutputHelper _outputHelper;
    private readonly HttpClient _client;

    public HighLevelTests(ITestOutputHelper outputHelper)
    {
      _outputHelper = outputHelper;
      
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
          //logging.SetMinimumLevel(LogLevel.Error); // comment out to use default log level => info 
          //logging.SetMinimumLevel(LogLevel.Debug);
        }));
      _client = server.CreateClient();
    }

    [Fact]
    public async Task GivenATestServe_RootShouldReturnGreenState()
    {
      var response = await _client.GetAsync("/");
      JsonDocument json = await GetJsonFromContent(response);
      var actual = json.RootElement.GetProperty("state").GetString();
      actual.ToLower().Should().Be("green");
    }

    [Fact]
    public async Task FlickShouldReturnLedObjectInNewState()
    {
      // Act
      var response = await _client.GetAsync("/api/led/3/_flick");

      // Assert
      response.EnsureSuccessStatusCode();
      JsonDocument json = await GetJsonFromContent(response);
      
      JsonElement id = json.RootElement.GetProperty("id");
      JsonElement state = json.RootElement.GetProperty("state");
      id.GetInt32().Should().Be(3);
      state.GetInt32().Should().Be(1);
    }

    private async Task<JsonDocument> GetJsonFromContent(HttpResponseMessage response)
    {
      var responseString = await response.Content.ReadAsStringAsync();
      _outputHelper.WriteLine(responseString);
      return JsonDocument.Parse(responseString);
    }
  }
}