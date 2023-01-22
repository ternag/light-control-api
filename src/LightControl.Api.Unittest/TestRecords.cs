using System;
using FluentAssertions;
using Xunit;

namespace LightControl.Api.UnitTest;

public class TestRecords
{
    [Fact]
    public void FactMethodName()
    {
        var id1 = new TestId(4);
        var id2 = new TestId(6);

        (id1 == id2).Should().BeFalse();
    }

    [Fact]
    public void FactMethodName3()
    {
        TestId id = 44;

        (id == 44).Should().BeTrue();
    }

    [Fact]
    public void FactMethodName2()
    {
        var id1 = new TestId(6);
        var id2 = (ushort)id1;

        id2.Should().Be(6);
    }

    [Fact]
    public void FactMethodName4()
    {
        TestId id1 = 4;
        TestId id2 = 8;

        (id1 >= id2).Should().BeFalse();
        (id1 <= id2).Should().BeTrue();
    }

    public record TestId(ushort Value)
    {
        public static implicit operator ushort(TestId value) => value.Value;
        public static implicit operator int(TestId value) => value.Value;
        public static implicit operator TestId(ushort value) => new TestId(value);
        public static implicit operator TestId(int value) => new TestId(Convert.ToUInt16(value));
        public static bool operator >(TestId a, TestId b) => a.Value > b.Value;
        public static bool operator <(TestId a, TestId b) => a.Value < b.Value;
        public static bool operator >=(TestId a, TestId b) => a.Value >= b.Value;
        public static bool operator <=(TestId a, TestId b) => a.Value <= b.Value;
    }
}
