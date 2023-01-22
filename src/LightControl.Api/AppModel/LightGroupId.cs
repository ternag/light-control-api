using LightControl.Api.Utils;

namespace LightControl.Api.AppModel;

public record LightGroupId(ushort Value) : IFormattable
{
    public string ToString(string? format, IFormatProvider? formatProvider) => Value.ToString(format, formatProvider);

    public static implicit operator LightGroupId(ushort value) => new(value);
    public static implicit operator LightGroupId(int value) => new(Convert.ToUInt16(value));
    public static implicit operator LightGroupId(string value) => new(Convert.ToUInt16(value, value.GetBase()));
    public static implicit operator ushort(LightGroupId value) => value.Value;
    public static implicit operator int(LightGroupId value) => value.Value;
    public static bool operator >(LightGroupId a, LightGroupId b) => a.Value > b.Value;
    public static bool operator <(LightGroupId a, LightGroupId b) => a.Value < b.Value;
    public static bool operator >=(LightGroupId a, LightGroupId b) => a.Value >= b.Value;
    public static bool operator <=(LightGroupId a, LightGroupId b) => a.Value <= b.Value;

    public override string ToString() => Value.ToString();
}
