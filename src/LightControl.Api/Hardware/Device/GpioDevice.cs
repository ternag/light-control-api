using System.Device.Gpio;
using LightControl.Api.Infrastructure;

namespace LightControl.Api.Hardware.Device
{
  public class GpioDevice : IDevice
  {
    GpioController _gpio;

    public GpioDevice()
    {
      _gpio = new GpioController();
    }

    public void Write(PinNumber pin, PinValue value)
    {
      _gpio.Write((int)pin, value);
    }

    public string Name => "Gpio";
  }
}