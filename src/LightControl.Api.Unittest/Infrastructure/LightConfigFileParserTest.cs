using System.IO;
using AutoFixture;
using FluentAssertions;
using LightControl.Api.Infrastructure;
using LightControl.Api.UnitTest.TestUtils;
using Xunit;
using Xunit.Abstractions;

namespace LightControl.Api.UnitTest.Infrastructure
{
  public class LightConfigFileParserTest
  {
    private readonly ITestOutputHelper _outputHelper;
    private readonly IFixture _fixture;

    public LightConfigFileParserTest(ITestOutputHelper outputHelper)
    {
      _outputHelper = outputHelper;
      _fixture = new CustomFixture();
    }

    [Fact]
    public void GivenValidJsonFile_CanDeserializeToDto()
    {
      var sut = _fixture.Create<LightConfigFileParser>();
      FileInfo fileInfo = new FileInfo("./File/ModularBuildings.json");
      var lightConfigDto = sut.Parse(fileInfo);
      lightConfigDto.Name.Should().Be("Modular Buildings");
      lightConfigDto.Groups.Should().HaveCount(2);
    }
  }
}