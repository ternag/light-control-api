using System;
using FluentAssertions;
using LightControl.Api.AppModel;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware;

public class PinNumberTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(42)]
    [InlineData(65535)]
    public void GivenValidInput_CanCreate(ushort pinNumber)
    {
        PinNumber actual = pinNumber;
        actual.Should().NotBeNull();
    }

    [Fact]
    public void ImplementsEqualEqualOperator()
    {
        PinNumber a = 123;
        PinNumber b = 123;
        PinNumber c = 4;

        (a == b).Should().BeTrue();
        (a < b).Should().BeFalse();
        (a > b).Should().BeFalse();
        (a <= b).Should().BeTrue();
        (a >= b).Should().BeTrue();
        (a < c).Should().BeFalse();
        (a > c).Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(65536)]
    public void GivenInvalidInput_CannotCreate(int invalidPinNumber)
    {
        PinNumber sut;
        Action act = () => sut = invalidPinNumber;
        act.Should().Throw<Exception>();
    }
}