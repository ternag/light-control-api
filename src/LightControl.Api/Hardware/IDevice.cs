using System;
using System.Device.Gpio;

namespace LightControl.Api.Hardware
{
  // ToDo: Implement IDisposable
  public interface IDevice : IDisposable
  {
    void Write(PinNumber pin, PinValue value);
    string DisplayName { get; }
    void InitPin(PinNumber pin);
  }
}