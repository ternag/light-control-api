using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using LightControl.Api.Hardware.Configuration;
using LightControl.Api.Hardware.ConfigurationTransferModel;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware;

public class HardwareInfoMapperTest
{
    private readonly IFixture _fixture;

    public HardwareInfoMapperTest()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    private HardwareInfo TestData => new()
    {
        Devices = new[]
        {
            new DeviceInfo
            {
                DeviceType = DeviceType.Gpio,
                Map = new List<MapInfo>
                {
                    new() { Id = "1", Pin = 4 },
                    new() { Id = "2", Pin = 17 },
                    new() { Id = "3", Pin = 27 },
                    new() { Id = "4", Pin = 19 }
                }
            },
            new DeviceInfo
            {
                DeviceType = DeviceType.Mcp23017,
                BusId = "1",
                DeviceId = "0x20",
                Map = new List<MapInfo>
                {
                    new() { Id = "0x10", Pin = 0 },
                    new() { Id = "0x11", Pin = 1 },
                    new() { Id = "0x12", Pin = 2 },
                    new() { Id = "0x13", Pin = 3 }
                }
            }
        }
    };

    [Fact]
    public void GivenSpecificHardwareInfo_ExpectToGetEightPinConfigurations()
    {
        var sut = _fixture.Create<HardwareInfoMapper>();
        var pins = sut.GetPins(TestData);
        pins.Count.Should().Be(8);
    }
}