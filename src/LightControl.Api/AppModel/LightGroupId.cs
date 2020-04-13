using System;
using LightControl.Api.Utils;

namespace LightControl.Api.AppModel
{
  public readonly struct LightGroupId : IEquatable<LightGroupId>, IComparable<LightGroupId>, IFormattable
  {
    private LightGroupId(ushort value)
    {
      _value = value;
    }

    private readonly ushort _value;
    public static implicit operator LightGroupId(ushort value) => new LightGroupId(value);
    public static implicit operator LightGroupId(int value) => new LightGroupId(Convert.ToUInt16(value));
    public static implicit operator LightGroupId(string value) => new LightGroupId(Convert.ToUInt16(value, value.GetBase()));
    public static explicit operator ushort(LightGroupId value) => value._value;
    public static explicit operator int(LightGroupId value) => value._value;

    public static bool operator ==(LightGroupId a, LightGroupId b) => a.Equals(b);
     public static bool operator !=(LightGroupId a, LightGroupId b) => !a.Equals(b);
    public static bool operator >(LightGroupId a, LightGroupId b) => a._value > b._value;
    public static bool operator <(LightGroupId a, LightGroupId b) => a._value < b._value;
    public static bool operator >=(LightGroupId a, LightGroupId b) => a._value >= b._value;
    public static bool operator <=(LightGroupId a, LightGroupId b) => a._value <= b._value;

    public bool Equals(LightGroupId other) => other._value == _value;

    public override bool Equals(object? obj)
    {
      if (obj is LightGroupId id)
      {
        return Equals(id);
      }

      return false;
    }

    public override int GetHashCode() => _value.GetHashCode();

    public int CompareTo(LightGroupId other)
    {
      return _value.CompareTo(other);
    }

    public override string ToString() => _value.ToString();

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
      return _value.ToString(format, formatProvider);
    }
  }
}