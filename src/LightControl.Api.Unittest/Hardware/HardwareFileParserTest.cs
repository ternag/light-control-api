using System.IO;
using AutoFixture;
using FluentAssertions;
using LightControl.Api.Hardware;
using LightControl.Api.Hardware.Configuration;
using LightControl.Api.UnitTest.TestUtils;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware
{
  public class HardwareFileParserTest
  {
    private IFixture _fixture;

    public HardwareFileParserTest()
    {
      _fixture = new CustomFixture();
    }

    [Fact]
    public void GivenHardwareJsonFile_ParsesToHardwareInfoStructure()
    {
      HardwareFileParser sut = _fixture.Create<HardwareFileParser>();
      FileInfo json = new FileInfo("./File/dummy_hardware.json");
      var hardwareInfo = sut.Parse(json);
      hardwareInfo.Devices.Should().HaveCount(2);
    }
  }
}