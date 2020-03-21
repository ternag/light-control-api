using System.Device.Gpio;

namespace LightControl.Api.Infrastructure.Device
{
  public class GpioDevice : IDevice
  {
    GpioController _gpio;

    public GpioDevice()
    {
      _gpio = new GpioController();
    }

    public void Write(int pin, PinValue value)
    {
      _gpio.Write(pin, value);
    }
  }
}