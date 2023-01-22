using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using LightControl.Api.Hardware.ConfigurationTransferModel;

namespace LightControl.Api.UnitTest.TestUtils;

public class CustomFixture : Fixture
{
    private readonly Random _random = new();

    public CustomFixture()
    {
        Customize(new AutoMoqCustomization());
        this.Register(() => new MapInfo($"0x{_random.Next(0, 0xFFFF):x4}", _random.Next(0, 255)));
    }
}