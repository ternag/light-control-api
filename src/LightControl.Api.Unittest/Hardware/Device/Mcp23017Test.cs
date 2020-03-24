using System;
using System.Collections;
using FluentAssertions;
using LightControl.Api.Hardware.Device;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware.Device
{
  public class Mcp23017Test
  {
    [Theory]
    [InlineData(0x0, 0, false, 0x0)]
    [InlineData(0x0, 0, true, 0x1)]
    [InlineData(0x1, 0, false, 0x0)]
    [InlineData(0x1, 0, true, 0x1)]
    [InlineData(0xFF, 8, true, 0x1FF)]
    [InlineData(0xFF, 9, true, 0x2FF)]
    [InlineData(0x8000, 15, false, 0x0000)]
    [InlineData(0x8000, 15, true, 0x8000)]
    [InlineData(0xFFFF, 7, false, 0xFF7F)]
    [InlineData(0xFFFF, 7, true, 0xFFFF)]
    public void GivenValidInput_PinIsSetToCorrectValue(ushort pinValues, byte pin, bool newState, ushort expected)
    {
      ushort actual = Mcp23017.SetBit(pinValues, pin, newState);
      actual.Should().Be(expected);
    }

    [Fact]
    public void GivenValidBitArray_ConvertsToUShort()
    {
      var ar = new BitArray(new byte[] {0xFF, 0x05 });
      var actual = Mcp23017.BitArrayToUshort(ar);
      actual.Should().Be(0x05FF);
    }
    
    [Fact]
    public void GivenNullInput_ShouldThrowArgumentNullException()
    {
      Func<ushort> func = () => Mcp23017.BitArrayToUshort(null);
      func.Should().Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void GivenToBigBitArray_ShouldThrowArgumentException()
    {
      var ar = new BitArray(new byte[] {0xFF, 0xFF, 0x1 });
      Func<ushort> func = () => Mcp23017.BitArrayToUshort(ar);
      func.Should().Throw<ArgumentException>();
    }
    
    
  }
}