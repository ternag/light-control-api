using System;

namespace LightControl.Api.Models
{
  public struct LedId
  {
    public LedId(ushort value)
    {
      _value = value;
    }

    private readonly ushort _value;

    public static implicit operator LedId(ushort value) => new LedId(value);
    public static implicit operator LedId(int value) => new LedId(Convert.ToUInt16(value));
    public static explicit operator ushort(LedId value) => value._value;
    public static explicit operator int(LedId value) => value._value;

    public static bool operator ==(LedId a, LedId b) => a.Equals(b);
    public static bool operator !=(LedId a, LedId b) => !a.Equals(b);

    public bool Equals(LedId other) => other._value == _value;
    public override bool Equals(object obj)
    {
      if (obj is LedId)
      {
        return Equals((LedId)obj);
      }
      return false;
    }
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString();
  }
}