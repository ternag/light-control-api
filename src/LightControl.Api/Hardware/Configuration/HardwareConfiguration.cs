using System;
using System.Collections.Generic;
using LightControl.Api.Domain;

namespace LightControl.Api.Hardware.Configuration
{
  public class HardwareConfiguration : IHardwareConfiguration
  {
    public HardwareConfiguration(Dictionary<LedId, Pin> pins)
    {
      _pins = pins ?? throw new ArgumentNullException(nameof(pins));
    }

    private readonly Dictionary<LedId, Pin> _pins;

    public Pin GetPin(LedId id)
    {
      if (_pins.ContainsKey(id))
        return _pins[id];
      else
        throw new ArgumentException(
          $"The LedId '{id}' is unknown. Make sure the id is registered in the hardware configuration");
    }
    public void Dispose()
    {
      foreach (KeyValuePair<LedId, Pin> pair in _pins)
      {
        pair.Value?.Device?.Dispose();
      }
    }
  }
}