using System;
using LightControl.Api.Hardware.ConfigurationTransferModel;
using LightControl.Api.Hardware.Device;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware.Configuration
{
  public class HardwareDeviceFactory : IHardwareDeviceFactory
  {
    private readonly ILogger _logger;

    public HardwareDeviceFactory(ILogger logger)
    {
      _logger = logger;
    }

    public IDevice Create(DeviceInfo device)
    {
      if(device == null)
        throw new ArgumentNullException(nameof(device));
      
      switch (device.DeviceType.ToLower())
      {
        case DeviceType.DummyDevice :
          return new DummyHardwareDevice(_logger);
        case DeviceType.Gpio :
          return new GpioDevice(_logger);
        case DeviceType.Mcp23017 :
          return new Mcp23017(new Mcp23017Address(device.DeviceIdAsUShort), device.BusIdAsUShort, _logger);
        default:
          throw new Exception($"Error creating device: Device '{device.DeviceType}' is unknown");
      }
    }
  }
}