using System.IO;
using AutoFixture;
using FluentAssertions;
using LightControl.Api.Hardware.Configuration;
using LightControl.Api.UnitTest.TestUtils;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware;

public class HardwareFileParserTest
{
    private readonly IFixture _fixture;

    public HardwareFileParserTest()
    {
        _fixture = new CustomFixture();
    }

    [Fact]
    public void GivenHardwareJsonFile_ParsesToHardwareInfoStructure()
    {
        var sut = _fixture.Create<HardwareFileParser>();
        var json = new FileInfo("./File/dummy_hardware.json");
        var hardwareInfo = sut.Parse(json);
        hardwareInfo.Devices.Should().HaveCount(2);
    }
}