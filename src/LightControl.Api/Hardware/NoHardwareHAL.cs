using LightControl.Api.Infrastructure;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware
{
  public class NoHardwareHal : IHal
  {
    private readonly ILogger<NoHardwareHal> _logger;
    private readonly IHardwareConfiguration _hardwareConfiguration;

    public NoHardwareHal(ILogger<NoHardwareHal> logger, IHardwareConfiguration hardwareConfiguration)
    {
      this._logger = logger;
      _hardwareConfiguration = hardwareConfiguration;
    }
    public void Update(Led led)
    {
      IDevice device = _hardwareConfiguration.GetDevice(led.Id);
      var pin = _hardwareConfiguration.GetPin(led.Id);
      _logger.LogInformation($"HAL: {led.Id}: {led.State} - '{led.Display}' - Device: {device.Name} - Pin: {pin}");
    }
  }
}