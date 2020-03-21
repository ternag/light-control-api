using System;

namespace LightControl.Api.Hardware
{
  public struct PinId
  {
    public PinId(ushort value)
    {
      _value = value;
    }

    private readonly ushort _value;

    public static implicit operator PinId(ushort value) => new PinId(value);
    public static implicit operator PinId(int value) => new PinId(Convert.ToUInt16(value));
    public static explicit operator ushort(PinId value) => value._value;
    public static explicit operator int(PinId value) => value._value;

    public static bool operator ==(PinId a, PinId b) => a.Equals(b);
    public static bool operator !=(PinId a, PinId b) => !a.Equals(b);

    public bool Equals(PinId other) => other._value == _value;
    public override bool Equals(object obj)
    {
      if (obj is PinId)
      {
        return Equals((PinId)obj);
      }
      return false;
    }
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString();
  }
}