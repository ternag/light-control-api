using System.Device.Gpio;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Infrastructure
{
  public class RaspberryPiGpioHAL : IHal
  {
    GpioController _gpio;
    HardwareConfiguration _hardwareConfiguration;
    private readonly ILogger<RaspberryPiGpioHAL> _logger;

    public RaspberryPiGpioHAL(ILogger<RaspberryPiGpioHAL> logger)
    {
      _logger = logger;
      _gpio = new GpioController();
      _hardwareConfiguration = new HardwareConfiguration();
      Init();
    }

    private void Init()
    {
      foreach (var led in _hardwareConfiguration.LEDs)
      {
          _gpio.OpenPin(led.Pin, PinMode.Output);
          _gpio.Write(led.Pin, PinValue.Low);
      }
    }

    public void Update(Led led)
    {
      int pin = _hardwareConfiguration.GetPin(led.Id);
      _gpio.Write(pin, led.State.ToPinValue());
    }
  }
}