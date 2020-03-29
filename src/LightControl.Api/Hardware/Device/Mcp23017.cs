using System;
using System.Collections;
using System.Device.Gpio;
using System.Device.I2c;
using Iot.Device.Mcp23xxx;
using LightControl.Api.Infrastructure;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware.Device
{
  // ToDo: Implement IDisposable
  public class Mcp23017 : IDevice
  {
    private readonly ILogger<IDevice> _logger;
    private readonly Iot.Device.Mcp23xxx.Mcp23017 _device;
    private ushort _pinValues = 0x0;

    public Mcp23017(Mcp23017Address address, ILogger<IDevice> logger)
    {
      _logger = logger;
      Address = address;
      I2cConnectionSettings settings = new I2cConnectionSettings(Bus, address);
      I2cDevice device = I2cDevice.Create(settings);
      _device = new Iot.Device.Mcp23xxx.Mcp23017(device);
      _device.WriteUInt16(Register.IODIR, 0x0); // init all 16 pins to output
      _device.WriteUInt16(Register.GPIO, 0x0); // init all 16 pins to low (zero)
    }

    public void InitPin(PinNumber pin)
    {
      // all pins are initialized in constructor for this device
    }

    public Mcp23017Address Address { get; }
    public int Bus => 1; // ToDo: Make configurable (Raspberry PI 3 uses Bus 1)  

    public void Write(PinNumber pin, PinValue value)
    {
      if (pin > 15)
        throw new ArgumentException(
          $"The Mcp23017 device can only handle pin number between 0 and 15. Provided PinNumber was {pin}");

      _logger.LogDebug($"Setting pin {pin:x}");
      _pinValues = SetBit(_pinValues, (ushort) pin, (bool) value);
      _device.WriteUInt16(Register.GPIO, _pinValues);
    }

    public static ushort SetBit(ushort pinValues, ushort pin, bool value)
    {
      byte[] bytes = BitConverter.GetBytes(pinValues);
      BitArray bitArray = new BitArray(bytes);
      bitArray.Set(pin, value);
      return BitArrayToUshort(bitArray);
    }

    public static ushort BitArrayToUshort(BitArray bitArray)
    {
      if (bitArray == null) throw new ArgumentNullException(nameof(bitArray));
      if (bitArray.Length > 16)
        throw new ArgumentException($"BitArray is too big to fit in a uInt16. Length was {bitArray.Length}");

      byte[] sh = new byte[2];
      bitArray.CopyTo(sh, 0);
      var uInt16 = BitConverter.ToUInt16(sh, 0);
      return uInt16;
    }

    public string DisplayName => "MCP23017";
  }
}