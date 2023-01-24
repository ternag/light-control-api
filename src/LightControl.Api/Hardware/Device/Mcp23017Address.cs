namespace LightControl.Api.Hardware.Device;

public class Mcp23017Address
{
    public Mcp23017Address(int address)
    {
        if (address < 0x20 || address > 0x27) throw new ArgumentException("DeviceId range is [0x20 .. 0x27]");
        Value = address;
    }

    public int Value { get; }

    public static implicit operator int(Mcp23017Address address) => address.Value;
    public static implicit operator Mcp23017Address(int address) => new(address);
}
