using System;

namespace LightControl.Api.Domain
{
  public interface IDevice : IDisposable
  {
    void Write(PinNumber pin, LedState value);
    string DisplayName { get; }
    void InitPin(PinNumber pin);
  }
}