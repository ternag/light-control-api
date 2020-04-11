using System;

namespace LightControl.Api.Domain
{
  public interface IDevice : IDisposable
  {
    void Write(PinNumber pin, LedState value);
    string DisplayName { get; } // TODO: Add a type name prop and add display name to hw config file 
    void InitPin(PinNumber pin);
  }
}