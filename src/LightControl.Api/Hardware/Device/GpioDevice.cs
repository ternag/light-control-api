using System.Device.Gpio;
using LightControl.Api.AppModel;
using LightControl.Api.Hardware.Extensions;

namespace LightControl.Api.Hardware.Device;

public sealed class GpioDevice : IDevice
{
    private readonly ILogger _logger;
    private readonly GpioController _gpio;

    public GpioDevice(ILogger logger)
    {
        _logger = logger;
        _gpio = new GpioController();
    }

    public void InitPin(PinNumber pin)
    {
        _gpio.OpenPin((int)pin, PinMode.Output);
        _gpio.Write((int)pin, PinValue.Low);
    }

    public void Write(PinNumber pin, LedState value)
    {
        _logger.LogDebug($"Writing {value} to pin {pin}");
        _gpio.Write((int)pin, value.ToPinValue());
    }

    public string DisplayName => "Gpio";

    public void Dispose()
    {
        _gpio?.Dispose();
    }
}
