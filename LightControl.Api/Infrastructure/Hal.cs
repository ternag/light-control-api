using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Gpio;
using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure 
{
  public interface IHal 
  {
    void Update(Led led);
  }

  public class Hal : IHal
  {
    GpioController _gpio;
    HardwareConfiguration _hardwareConfiguration;

    public Hal()
    {
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