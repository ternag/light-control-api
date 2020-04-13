using System;

namespace LightControl.Api.AppModel
{
  public readonly struct PinNumber : IEquatable<PinNumber>, IFormattable
  {
    private PinNumber(ushort value)
    {
      _value = value;
    }

    private readonly ushort _value;

    public static implicit operator PinNumber(ushort value) => new PinNumber(value);
    public static implicit operator PinNumber(int value) => new PinNumber(Convert.ToUInt16(value));
    public static explicit operator ushort(PinNumber value) => value._value;
    public static explicit operator int(PinNumber value) => value._value;

    public static bool operator ==(PinNumber a, PinNumber b) => a.Equals(b);
    public static bool operator !=(PinNumber a, PinNumber b) => !a.Equals(b);
    public static bool operator >(PinNumber a, PinNumber b) => a._value > b._value;
    public static bool operator <(PinNumber a, PinNumber b) => a._value < b._value;
    public static bool operator >=(PinNumber a, PinNumber b) => a._value >= b._value;
    public static bool operator <=(PinNumber a, PinNumber b) => a._value <= b._value;


    public bool Equals(PinNumber other) => other._value == _value;

    public override bool Equals(object obj)
    {
      if (obj is PinNumber)
      {
        return Equals((PinNumber) obj);
      }

      return false;
    }

    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => _value.ToString();

    public string ToString(string format, IFormatProvider formatProvider)
    {
      return _value.ToString(format, formatProvider);
    }
  }
}