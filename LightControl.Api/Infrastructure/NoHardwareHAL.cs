using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Infrastructure
{
  public class NoHardwareHAL : IHal
  {
    private readonly ILogger<NoHardwareHAL> _logger;

    public NoHardwareHAL(ILogger<NoHardwareHAL> logger)
    {
      this._logger = logger;
    }
    public void Update(Led led)
    {
      _logger.LogInformation($"HAL: {led.Id}: {led.State} - '{led.Display}'");
    }
  }
}