#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace LightControl.Api.Models
{
  public readonly struct LedId
  {
    public LedId(ushort mValue)
    {
      m_value = mValue;
    }

    private readonly ushort m_value;
    public static implicit operator LedId(ushort value) => new LedId(value);
    public static implicit operator LedId(int value) => new LedId(Convert.ToUInt16(value));
    public static explicit operator ushort(LedId value) => value.m_value;
    public static explicit operator int(LedId value) => value.m_value;

    public static bool operator ==(LedId a, LedId b) => a.Equals(b);
    public static bool operator !=(LedId a, LedId b) => !a.Equals(b);
    public static bool operator >(LedId a, LedId b) => a.m_value > b.m_value;
    public static bool operator <(LedId a, LedId b) => a.m_value < b.m_value;
    public static bool operator >=(LedId a, LedId b) => a.m_value >= b.m_value;
    public static bool operator <=(LedId a, LedId b) => a.m_value <= b.m_value;

    public bool Equals(LedId other) => other.m_value == m_value;
    public override bool Equals(object obj)
    {
      if (obj is LedId id)
      {
        return Equals(id);
      }
      return false;
    }
    public override int GetHashCode() => m_value.GetHashCode();
    
    public override string ToString() => m_value.ToString();
    public string ToString(IFormatProvider? provider) => m_value.ToString(provider);
    public string ToString(string? format) => m_value.ToString(format);
    public string ToString(string? format, IFormatProvider? provider) => m_value.ToString(format, provider);
  }
}