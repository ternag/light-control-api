using System.Device.I2c;
using LightControl.Api.AppModel;
using Iot.Device.Pwm;

namespace LightControl.Api.Hardware.Device;

/// <summary>
/// Address range 0x40 -> 0x7F
/// </summary>
public sealed class Pca9685Device : IDevice
{
    private readonly ILogger _logger;
    private readonly Pca9685 _device;

    public Pca9685Device(ushort bus, Pca9685Address address, ILogger logger)
    {
        _logger = logger;
        var settings = new I2cConnectionSettings(bus, address);
        var i2CDevice = I2cDevice.Create(settings);
        _device = new Pca9685(i2CDevice);
    }

    public string DisplayName => "PCA9685";

    public void Write(PinNumber pin, LedState value)
    {
        _device.SetDutyCycle(pin, value == LedState.Off ? 0.0 : 1.0);
    }

    public void Write(PinNumber pin, double value)
    {
        _device.SetDutyCycle(pin, value);
    }

    public void InitPin(PinNumber pin)
    {
        _device.SetDutyCycle(pin,0.0);
    }

    public void Dispose()
    {
        _device?.Dispose();
    }
}
