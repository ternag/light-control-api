using System;
using System.Device.Gpio;

namespace LightControl.Api.Hardware
{
  public interface IDevice : IDisposable
  {
    void Write(PinNumber pin, PinValue value);
    string DisplayName { get; }
    void InitPin(PinNumber pin);
  }
}