using System;
using FluentAssertions;
using LightControl.Api.Models;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware
{
  public class PinIdTest
  {
    [Theory]
    [InlineData(0)]
    [InlineData(42)]
    [InlineData(65535)]
    public void GivenValidInput_CanCreate(int pinId)
    {
      LedId sut = pinId;
      Assert.Equal(sut, pinId);
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
}
