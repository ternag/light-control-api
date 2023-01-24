namespace LightControl.Api.Hardware.Device;

public class Pca9685Address
{
    public Pca9685Address(int address)
    {
        if (address < 0x40 || address > 0x7E) throw new ArgumentException("DeviceId range is [0x40 .. 0x7E]");
        Value = address;
    }

    public int Value { get; }

    public static implicit operator int(Pca9685Address address) => address.Value;
    public static implicit operator Pca9685Address(int address) => new(address);
}