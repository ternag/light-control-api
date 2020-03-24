using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LightControl.Api.Hardware.Device;
using LightControl.Api.Infrastructure;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware
{
  public interface IHardwareConfiguration
  {
    IDevice GetDevice(LedId id);
    PinNumber GetPin(LedId id);
  }

  public class HardwareConfiguration : IHardwareConfiguration
  {
    public HardwareConfiguration(Dictionary<LedId, IDevice> devices, Dictionary<LedId, PinNumber> pins,
      ILogger<Hal> logger)
    {
      // TODO: Inject hardware configuration via constructor
      _devices = devices;
      _pins = pins;
      _logger = logger;
    }

    private readonly Dictionary<LedId, IDevice> _devices;
    private readonly Dictionary<LedId, PinNumber> _pins;
    private readonly ILogger<Hal> _logger;

    public IDevice GetDevice(LedId id)
    {
      if (_devices.ContainsKey(id))
        return _devices[id];
      else
        throw new ArgumentException(
          $"The Pin id '{id}' is unknown. Make sure the id is registered in the hardware configuration");
    }

    public PinNumber GetPin(LedId id)
    {
      if (_pins.ContainsKey(id))
        return _pins[id];
      else
        throw new ArgumentException(
          $"The Pin id '{id}' is unknown. Make sure the id is registered in the hardware configuration");
    }
  }
}