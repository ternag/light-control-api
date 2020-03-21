using LightControl.Api.Infrastructure;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware
{
  public class Hal : IHal
  {
    private readonly ILogger<Hal> _logger;
    private HardwareConfiguration _hw;

    public Hal(ILogger<Hal> logger)
    {
      _logger = logger;
      _hw = new HardwareConfiguration(_logger);
    }

    public void Update(Led led)
    {
      IDevice device = _hw.GetDevice(led.Id);
      var pin = _hw.GetPin(led.Id);
      device.Write(pin, led.State.ToPinValue());
    }
  }
}