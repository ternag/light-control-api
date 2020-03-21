using System.Device.Gpio;
using LightControl.Api.Hardware;

namespace LightControl.Api.Infrastructure.Device
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
  }
}