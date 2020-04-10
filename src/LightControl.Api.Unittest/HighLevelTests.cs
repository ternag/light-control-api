using System.Collections;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using LightControl.Api.UnitTest.TestUtils;
using Microsoft.Extensions.Logging;
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
      var server = TestServerFactory.Create(outputHelper, LogLevel.Error);
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

    [Fact(Skip = "Until implemented")]
    public async Task ApiRootShouldRespondOk()
    {
      // Act
      var response = await _client.GetAsync("/api/");
      
      // Assert
      response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task LedShouldRespondWithLedStatus()
    {
      // Act
      var response = await _client.GetAsync("/api/led/2");
      
      // Assert
      response.EnsureSuccessStatusCode();
      JsonDocument json = await GetJsonFromContent(response);
      
      JsonElement id = json.RootElement.GetProperty("id");
      JsonElement state = json.RootElement.GetProperty("state");
      id.GetInt32().Should().Be(2);
      state.GetInt32().Should().Be(0);
    }
    
    [Fact]
    public async Task GetAllLedShouldRespondWithListOfLeds()
    {
      // Act
      var response = await _client.GetAsync("/api/led");
      
      // Assert
      response.EnsureSuccessStatusCode();
      JsonDocument json = await GetJsonFromContent(response);
      json.RootElement.ValueKind.Should().Be(JsonValueKind.Array);
      json.RootElement.EnumerateArray().Should().HaveCount(24);
    }

    
    
    private async Task<JsonDocument> GetJsonFromContent(HttpResponseMessage response)
    {
      var responseString = await response.Content.ReadAsStringAsync();
      //_outputHelper.WriteLine(responseString);
      var responseJson = JsonDocument.Parse(responseString);
      _outputHelper.WriteLine(responseJson.ToFormatedString());
      return responseJson;
    }

  }
}