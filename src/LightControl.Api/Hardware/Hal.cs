using LightControl.Api.Hardware.Configuration;
using LightControl.Api.Hardware.Extensions;
using LightControl.Api.Models;

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

    public void Dispose()
    {
      _hardwareConfiguration?.Dispose();
    }
  }
}