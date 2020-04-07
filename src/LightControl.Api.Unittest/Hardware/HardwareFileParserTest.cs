using System.IO;
using FluentAssertions;
using LightControl.Api.Infrastructure.Hardware;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware
{
  public class HardwareFileParserTest
  {
    [Fact]
    public void GivenHardwareJsonFile_ParsesToHardwareInfoStructure()
    {
      HardwareFileParser sut = new HardwareFileParser();
      FileInfo json = new FileInfo("./Hardware/hardware.json");
      var hardwareInfo = sut.Parse(json);
      hardwareInfo.Devices.Should().HaveCount(2);
    }

    [Fact]
    public void Test()
    {
      HardwareFileParser sut = new HardwareFileParser();
      sut.Serialize();
    }
  }
}