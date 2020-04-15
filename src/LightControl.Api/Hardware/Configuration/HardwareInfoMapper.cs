using System;
using System.Collections.Generic;
using LightControl.Api.AppModel;
using LightControl.Api.Hardware.ConfigurationTransferModel;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware.Configuration
{
  public class HardwareInfoMapper : IHardwareInfoMapper
  {
    private readonly IHardwareDeviceFactory _hardwareDeviceFactory;
    private readonly ILogger _logger;

    public HardwareInfoMapper(IHardwareDeviceFactory hardwareDeviceFactory, ILogger<HardwareInfoMapper> logger)
    {
      _hardwareDeviceFactory = hardwareDeviceFactory;
      _logger = logger;
    }

    public Dictionary<LedId, Pin> GetPins(IHardwareInfo hardwareInfo)
    {
      var pins = new Dictionary<LedId, Pin>();
      
      foreach(DeviceInfo device in hardwareInfo.Devices)
      {
        try
        {
          IDevice concreteDevice = _hardwareDeviceFactory.Create(device);

          foreach (MapInfo mapInfo in device.Map)
          {
            pins.Add(mapInfo.Id, new Pin(mapInfo.Pin, concreteDevice));
          }
        }
        catch (Exception e)
        {
          // Here we just log that a device was not created properly. Maybe communication failed or maybe 
          // the device is not physically connected even though it is configured in the hardware configuration.
          // We catch the exception and then we move on to try to create the other devices in the configuration.
          // TODO: Implement some sort of status over devices. Maybe a NotConnectedDevice or ErrorDevice.
          // Some sort of status that can be reported back to the client. 
          _logger.LogError($"Error creating and mapping Device {device.DeviceType} {device.DeviceId}", e);
        }
      }

      return pins;
    }
  }
}