namespace LightControl.Api.AppModel;

public record PinNumber(ushort Value) : IFormattable
{
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return Value.ToString(format, formatProvider);
    }

    public static implicit operator PinNumber(ushort value) => new(value);
    public static implicit operator PinNumber(int value) => new(Convert.ToUInt16(value));
    public static implicit operator ushort(PinNumber value) => value.Value;
    public static implicit operator int(PinNumber value) => value.Value;
    public static bool operator >(PinNumber a, PinNumber b) => a.Value > b.Value;
    public static bool operator <(PinNumber a, PinNumber b) => a.Value < b.Value;
    public static bool operator >=(PinNumber a, PinNumber b) => a.Value >= b.Value;
    public static bool operator <=(PinNumber a, PinNumber b) => a.Value <= b.Value;
}
