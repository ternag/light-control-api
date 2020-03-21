using LightControl.Api.Hardware;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Infrastructure
{
  public class NoHardwareHAL : IHal
  {
    private readonly ILogger<NoHardwareHAL> _logger;
    private HardwareConfiguration _hw;

    public NoHardwareHAL(ILogger<NoHardwareHAL> logger)
    {
      this._logger = logger;
      _hw = new HardwareConfiguration(_logger);
    }
    public void Update(Led led)
    {
      IDevice device = _hw.GetDevice(led.Id);
      var pin = _hw.GetPin(led.Id);
      _logger.LogInformation($"HAL: {led.Id}: {led.State} - '{led.Display}' - Device: {device.Name} - Pin: {pin}");
    }
  }
}