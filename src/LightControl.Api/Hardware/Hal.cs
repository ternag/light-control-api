using LightControl.Api.Infrastructure;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware
{
  public class Hal : IHal
  {
    private readonly IHardwareConfiguration _hardwareConfiguration;

    public Hal(IHardwareConfiguration hardwareConfiguration)
    {
      _hardwareConfiguration = hardwareConfiguration;
    }

    public void Update(Led led)
    {
      IDevice device = _hardwareConfiguration.GetDevice(led.Id);
      var pin = _hardwareConfiguration.GetPin(led.Id);
      device.Write(pin, led.State.ToPinValue());
    }
  }
}