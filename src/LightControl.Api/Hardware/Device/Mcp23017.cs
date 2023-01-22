using System.Collections;
using System.Device.I2c;
using Iot.Device.Mcp23xxx;
using LightControl.Api.AppModel;
using LightControl.Api.Hardware.Extensions;

namespace LightControl.Api.Hardware.Device;

public sealed class Mcp23017 : IDevice
{
    private readonly Iot.Device.Mcp23xxx.Mcp23017 _device;
    private readonly ILogger _logger;
    private ushort _pinValues;

    // TODO: Create type for 'Bus'
    public Mcp23017(Mcp23017Address address, ushort bus, ILogger logger)
    {
        _logger = logger;
        var settings = new I2cConnectionSettings(bus, address);
        var device = I2cDevice.Create(settings);
        _device = new Iot.Device.Mcp23xxx.Mcp23017(device);
        _device.WriteUInt16(Register.IODIR, 0x0); // init all 16 pins to output
        _device.WriteUInt16(Register.GPIO, 0x0); // init all 16 pins to low (zero)
    }

    public void InitPin(PinNumber pin)
    {
        // all pins are initialized in constructor for this device
    }

    public void Write(PinNumber pin, LedState value)
    {
        if (pin > 15)
            throw new ArgumentException(
                $"The Mcp23017 device can only handle pin number between 0 and 15. Provided PinNumber was {pin}");

        _logger.LogDebug($"Setting pin {pin:x} to {value}");
        _pinValues = SetBit(_pinValues, (ushort)pin, (bool)value.ToPinValue());
        _device.WriteUInt16(Register.GPIO, _pinValues);
    }

    public string DisplayName => "MCP23017";

    public void Dispose()
    {
        _device?.Dispose();
    }

    public static ushort SetBit(ushort pinValues, ushort pin, bool value)
    {
        var bytes = BitConverter.GetBytes(pinValues);
        var bitArray = new BitArray(bytes);
        bitArray.Set(pin, value);
        return BitArrayToUshort(bitArray);
    }

    public static ushort BitArrayToUshort(BitArray bitArray)
    {
        if (bitArray == null) throw new ArgumentNullException(nameof(bitArray));
        if (bitArray.Length > 16)
            throw new ArgumentException($"BitArray is too big to fit in a uInt16. Length was {bitArray.Length}");

        var sh = new byte[2];
        bitArray.CopyTo(sh, 0);
        var uInt16 = BitConverter.ToUInt16(sh, 0);
        return uInt16;
    }
}
