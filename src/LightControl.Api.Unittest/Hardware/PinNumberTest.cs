using System;
using Xunit;
using Xunit.Abstractions;
using LightControl.Api.Hardware;
using FluentAssertions;

namespace LightControl.Api.Unittest
{
  public class PinNumberTest
  {
    [Theory]
    [InlineData(0)]
    [InlineData(42)]
    [InlineData(65535)]
    public void CanCreate(int pinNumber)
    {
      PinNumber sut = pinNumber;
      Assert.Equal(sut, pinNumber);
    }

    [Fact]
    public void ImplementsEqualEqualOperator()
    {
      PinNumber a = 123;
      Assert.True(a == 123);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(65536)]
    public void CannotCreateNegativePinNumber(int invalidPinNumber)
    {
        PinNumber sut;
        Action act = () => sut = invalidPinNumber;
        act.Should().Throw<Exception>();
    }
  }
}
