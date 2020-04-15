using System;
using FluentAssertions;
using LightControl.Api.AppModel;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware
{
  public class PinNumberTest
  {
    [Theory]
    [InlineData(0)]
    [InlineData(42)]
    [InlineData(65535)]
    public void GivenValidInput_CanCreate(ushort pinNumber)
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
    public void GivenInvalidInput_CannotCreate(int invalidPinNumber)
    {
      PinNumber sut;
      Action act = () => sut = invalidPinNumber;
      act.Should().Throw<Exception>();
    }
  }
}