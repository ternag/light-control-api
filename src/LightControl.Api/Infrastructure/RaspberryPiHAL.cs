using System.Device.Gpio;
using System.Device.I2c;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Infrastructure
{
  public class RaspberryPiHAL : IHal
  {
    private readonly ILogger<RaspberryPiHAL> _logger;
    private HardwareConfiguration _hw;

    public RaspberryPiHAL(ILogger<RaspberryPiHAL> logger)
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