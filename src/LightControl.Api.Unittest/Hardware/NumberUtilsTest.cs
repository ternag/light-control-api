using FluentAssertions;
using LightControl.Api.Utils;
using Xunit;

namespace LightControl.Api.UnitTest.Hardware
{
  public class NumberUtilsTest
  {
    [Theory]
    [InlineData(null, 10)]
    [InlineData("", 10)]
    [InlineData("0", 10)]
    [InlineData("1000", 10)]
    [InlineData("0x1000", 16)]
    [InlineData("0x0", 16)]
    [InlineData("0xFFFF", 16)]
    [InlineData("0b0", 2)]
    [InlineData("0b0100_1010", 2)]
    public void TestDescription(string value, int expected)
    {
      int actual = value.GetBase();
      actual.Should().Be(expected);
    }
  }
}