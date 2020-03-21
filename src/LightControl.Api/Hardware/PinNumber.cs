using System;

namespace LightControl.Api.Hardware
{
  public readonly struct PinNumber : IEquatable<PinNumber>
  {
    public PinNumber(ushort value)
    {
      _value = value;
    }
    private readonly ushort _value;

    public static implicit operator PinNumber(ushort value) => value;
    public static implicit operator PinNumber(int value) => Convert.ToUInt16(value);
    public static explicit operator ushort(PinNumber value) => value._value;
    public static explicit operator int(PinNumber value) => value._value;

    public static bool operator ==(PinNumber a, PinNumber b) => a.Equals(b);
    public static bool operator !=(PinNumber a, PinNumber b) => !a.Equals(b);

    public bool Equals(PinNumber other) => other._value == _value;
    public override bool Equals(object obj)
    {
      if(obj is PinNumber)
      {
        return Equals((PinNumber)obj);
      }
      return false;
    }
    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString();
  }
}