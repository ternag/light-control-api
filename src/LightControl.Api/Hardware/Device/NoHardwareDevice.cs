using System.Device.Gpio;
using LightControl.Api.Infrastructure;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware.Device
{
  // ToDo: Implement IDisposable
  public class NoHardwareDevice : IDevice
  {
    private readonly ILogger _logger;

    public NoHardwareDevice(ILogger logger)
    {
      _logger = logger;
    }

    public void Write(PinNumber pin, PinValue value)
    {
      _logger.LogInformation($"Writing '{value}' to pin '{pin}'");
    }

    public string DisplayName => "No hardware device";
    public void InitPin(PinNumber pin)
    {
      _logger.LogInformation($"pin {pin} initialized");
    }
  }
}