using System.Device.Gpio;
using LightControl.Api.Hardware;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Infrastructure.Device
{
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

    public string Name => "No hardware device";
  }
}