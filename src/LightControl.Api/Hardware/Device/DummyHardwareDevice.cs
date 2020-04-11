using System.Device.Gpio;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware.Device
{
  /// <summary>
  /// This 
  /// </summary>
  public class DummyHardwareDevice : IDevice
  {
    private readonly ILogger _logger;

    public DummyHardwareDevice(ILogger logger)
    {
      _logger = logger;
    }

    public void Write(PinNumber pin, PinValue value)
    {
      _logger.LogInformation($"Writing '{value}' to pin '{pin:x}'");
    }

    public string DisplayName => "Dummy hardware device";

    public void InitPin(PinNumber pin)
    {
      _logger.LogDebug($"pin {pin} initialized");
    }

    public void Dispose()
    {
      // Nothing to see here. Move along.
    }
  }
}