using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using LightControl.Api.UnitTest.TestUtils;
using Xunit;
using Xunit.Abstractions;

namespace LightControl.Api.UnitTest;

public class HighLevelTests
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _outputHelper;

    public HighLevelTests(ITestOutputHelper outputHelper)
    {
        var factory = new TestServerFactory<Program>(outputHelper);
        _outputHelper = outputHelper;
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/api/led")]
    [InlineData("/api/led/1")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType?.ToString());

        _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }


    [Fact]
    public async Task GivenATestServe_RootShouldReturnGreenState()
    {
        var response = await _client.GetAsync("/");
        var json = await GetJsonFromContent(response);
        var actual = json.RootElement.GetProperty("state").GetString();
        actual.Should().NotBeNull();
        actual?.ToLower().Should().Be("green");
    }

    [Fact]
    public async Task FlickShouldReturnLedObjectInNewState()
    {
        // Act
        var response = await _client.GetAsync("/api/led/3/_flick");

        // Assert
        response.EnsureSuccessStatusCode();
        var json = await GetJsonFromContent(response);

        var id = json.RootElement.GetProperty("id");
        var state = json.RootElement.GetProperty("state");
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
        var json = await GetJsonFromContent(response);

        var id = json.RootElement.GetProperty("id");
        var state = json.RootElement.GetProperty("state");
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
        var json = await GetJsonFromContent(response);
        json.RootElement.ValueKind.Should().Be(JsonValueKind.Array);
        json.RootElement.EnumerateArray().Should().NotBeEmpty();
        json.RootElement.EnumerateArray().Should().NotContainNulls();
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
