using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using LightControl.Api.Hardware;
using LightControl.Api.Infrastructure.Hardware;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware
{
  public class HardwareInfoMapperTest
  {
    private readonly IFixture _fixture;

    public HardwareInfoMapperTest()
    {
      _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Fact]
    public void GivenSpecificHardwareInfo_ExpectToGetEightPinConfigurations()
    {
      HardwareInfoMapper sut = _fixture.Create<HardwareInfoMapper>(); 
      var pins = sut.GetPins(TestData);
      pins.Count.Should().Be(8);
    }

    [Fact]
    public void GivenSpecificHardwareInfo_ExpectToGetTwoDeviceConfigurations()
    {
      
      HardwareInfoMapper sut = _fixture.Create<HardwareInfoMapper>(); 
      var devices = sut.GetDevices(TestData);
      devices.Count.Should().Be(8);
    }
    
    private HardwareInfo TestData => new HardwareInfo
    {
      Devices = new DeviceInfo[]
      {
        new DeviceInfo()
        {
          DeviceType = DeviceType.Gpio,
          Map = new List<MapInfo>
          {
            new MapInfo{Id = "1", Pin = 4},
            new MapInfo{Id = "2", Pin = 17},
            new MapInfo{Id = "3", Pin = 27},
            new MapInfo{Id = "4", Pin = 19}
          }
        },
        new DeviceInfo
        {
          DeviceType = DeviceType.Mcp23017,
          BusId = "1",
          DeviceId = "0x20",
          Map = new List<MapInfo>
          {
            new MapInfo{Id = "0x10", Pin = 0},
            new MapInfo{Id = "0x11", Pin = 1},
            new MapInfo{Id = "0x12", Pin = 2},
            new MapInfo{Id = "0x13", Pin = 3},
          }
        }
      }
    }; 
  }
}