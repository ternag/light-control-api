namespace LightControl.Api.Hardware.Device;

public class Mcp23017Address
{
    public Mcp23017Address(int id)
    {
        if (id < 0x20 || id > 0x27) throw new ArgumentException("DeviceId range is [0x20 .. 0x27]");
        Value = id;
    }

    public int Value { get; }

    public static implicit operator int(Mcp23017Address id)
    {
        return id.Value;
    }

    public static explicit operator Mcp23017Address(int value)
    {
        return new Mcp23017Address(value);
    }
}