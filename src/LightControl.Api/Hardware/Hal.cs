using LightControl.Api.Infrastructure;
using LightControl.Api.Infrastructure.Hardware;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware
{
  // ToDo: Implement IDisposable (_hardwareConfiguration)
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