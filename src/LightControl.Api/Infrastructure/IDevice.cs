using System.Device.Gpio;
using LightControl.Api.Hardware;

namespace LightControl.Api.Infrastructure
{
  public interface IDevice
  {
    void Write(PinNumber pin, PinValue value);
    string Name { get; }
    void InitPin(PinNumber pin);
  }
}