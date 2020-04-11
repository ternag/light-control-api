using System;
using System.Collections.Generic;
using LightControl.Api.Domain;

namespace LightControl.Api.Hardware.Configuration
{
  public class HardwareConfiguration : IHardwareConfiguration
  {
    public HardwareConfiguration(Dictionary<LedId, Pin> pins2)
    {
      _pins2 = pins2 ?? throw new ArgumentNullException(nameof(pins2));
    }

    private readonly Dictionary<LedId, Pin> _pins2;

    public Pin GetPin(LedId id)
    {
      if (_pins2.ContainsKey(id))
        return _pins2[id];
      else
        throw new ArgumentException(
          $"The LedId '{id}' is unknown. Make sure the id is registered in the hardware configuration");
    }
    public void Dispose()
    {
      foreach (KeyValuePair<LedId, Pin> pair in _pins2)
      {
        pair.Value?.Device?.Dispose();
      }
    }
  }
}