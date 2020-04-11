using System;
using System.Collections.Generic;
using LightControl.Api.Domain;

namespace LightControl.Api.Hardware.Configuration
{
  public interface IHardwareConfiguration : IDisposable
  {
    IDevice GetDevice(LedId id);
    PinNumber GetPin(LedId id);
  }

  public class HardwareConfiguration : IHardwareConfiguration
  {
    public HardwareConfiguration(Dictionary<LedId, IDevice> devices, Dictionary<LedId, PinNumber> pins)
    {
      _devices = devices;
      _pins = pins;
    }

    private readonly Dictionary<LedId, IDevice> _devices;
    private readonly Dictionary<LedId, PinNumber> _pins;

    public IDevice GetDevice(LedId id)
    {
      if (_devices.ContainsKey(id))
        return _devices[id];
      else
        throw new ArgumentException(
          $"The LedId '{id}' is unknown. Make sure the id is registered in the hardware configuration");
    }

    public PinNumber GetPin(LedId id)
    {
      if (_pins.ContainsKey(id))
        return _pins[id];
      else
        throw new ArgumentException(
          $"The LedId '{id}' is unknown. Make sure the id is registered in the hardware configuration");
    }

    public void Dispose()
    {
      foreach (var pair in _devices)
      {
        pair.Value?.Dispose();
      }
    }
  }
}