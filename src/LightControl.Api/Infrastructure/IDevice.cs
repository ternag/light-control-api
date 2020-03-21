using System.Device.Gpio;

namespace LightControl.Api.Infrastructure
{
  public interface IDevice 
  {
    void Write(int pin, PinValue value);
  }
}