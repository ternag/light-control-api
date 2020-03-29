using System.Device.Gpio;
using LightControl.Api.Hardware;

namespace LightControl.Api.Infrastructure
{
  // ToDo: Implement IDisposable
  public interface IDevice
  {
    void Write(PinNumber pin, PinValue value);
    string DisplayName { get; }
    void InitPin(PinNumber pin);
  }
}