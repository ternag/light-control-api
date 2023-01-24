using LightControl.Api.AppModel;
using LightControl.Api.Hardware.Extensions;

namespace LightControl.Api.Hardware.Device;

/// <summary>
///     The purpose of this class is to be used for development and
///     testing on systems that do not support the System.Device.Gpio devices.
/// </summary>
public sealed class DummyHardwareDevice : IDevice
{
    private readonly ILogger _logger;

    public DummyHardwareDevice(ILogger logger)
    {
        _logger = logger;
    }

    public void Write(PinNumber pin, LedState value)
    {
        _logger.LogInformation($"Writing '{value.ToPinValue()}' to pin '{pin:x}'");
    }

    public string DisplayName => "Dummy hardware device";

    public void InitPin(PinNumber pin)
    {
        _logger.LogDebug($"pin {pin} initialized");
    }

    public void Write(PinNumber pin, double value)
    {
        _logger.LogDebug($"Writing '{value:F}' to pin '{pin:x}'");
    }

    public void Dispose()
    {
        // You can go about your business. Move along.
    }
}
