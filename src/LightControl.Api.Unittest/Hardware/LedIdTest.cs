using System;
using FluentAssertions;
using LightControl.Api.AppModel;
using Xunit;
using Xunit.Abstractions;

namespace LightControl.Api.UnitTest.Hardware;

public class PinIdTest
{
    private readonly ITestOutputHelper _outputHelper;

    public PinIdTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(42)]
    [InlineData(65535)]
    public void GivenValidInput_CanCreate(int pinId)
    {
        LedId sut = pinId;
        Assert.Equal(sut, pinId);
    }

    [Theory]
    [InlineData("0", 0)]
    [InlineData("0x20", 32)]
    [InlineData("0xffff", 65535)]
    public void GivenValidStringInput_CanCreate(string pinId, int expected)
    {
        LedId sut = pinId;
        Assert.Equal(sut, expected);
    }

    [Fact]
    public void ImplementsEqualEqualOperator()
    {
        LedId a = 123;
        Assert.True(a == 123);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(65536)]
    public void GivenInvalidInput_CannotCreate(int invalidPinId)
    {
        LedId sut;
        Action act = () => sut = invalidPinId;
        act.Should().Throw<Exception>();
    }
}