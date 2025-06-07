using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using LightControl.Api.Hardware.Configuration;
using LightControl.Api.Hardware.ConfigurationTransferModel;
using LightControl.Api.Hardware.Device;
using LightControl.Api.UnitTest.TestUtils;
using Xunit;
using Xunit.Abstractions;

namespace LightControl.Api.UnitTest.Hardware;

public class HardwareDeviceFactoryTest
{
    private readonly IFixture _fixture;
    private readonly ITestOutputHelper _outputHelper;

    public HardwareDeviceFactoryTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _fixture = new CustomFixture();
    }

    private DeviceInfo NoHardwareDevice => new()
    {
        DeviceType = DeviceType.DummyDevice,
        Map = _fixture.CreateMany<MapInfo>().ToList()
    };

    [Fact]
    public void NoHardwareDevice_IsCreated()
    {
        var sut = _fixture.Create<HardwareDeviceFactory>();
        var device = sut.Create(NoHardwareDevice);
        device.Should().BeOfType<DummyHardwareDevice>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("Plain old unknown hardware device")]
    public void IllegalDeviceTypeThrows(string deviceType)
    {
        var deviceInfo = new DeviceInfo
        {
            DeviceType = deviceType,
            Map = new List<MapInfo>()
        };
        var sut = _fixture.Create<HardwareDeviceFactory>();
        sut.Invoking(x => x.Create(deviceInfo))
            .Should().Throw<Exception>();
    }
}