using System.Collections.Generic;
using LightControl.Api.Hardware;
using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure.Hardware
{
  public class HardwareInfoMapper : IHardwareInfoMapper
  {
    private readonly IHardwareDeviceFactory _hardwareDeviceFactory;

    public HardwareInfoMapper(IHardwareDeviceFactory hardwareDeviceFactory)
    {
      _hardwareDeviceFactory = hardwareDeviceFactory;
    }

    public Dictionary<LedId, IDevice> GetDevices(IHardwareInfo hardwareInfo)
    {
      var devices = new Dictionary<LedId, IDevice>();

      foreach (DeviceInfo device in hardwareInfo.Devices)
      {
        IDevice concreteDevice = _hardwareDeviceFactory.Create(device);
        
        foreach (MapInfo mapInfo in device.Map)
        {
          devices.Add(mapInfo.Id, concreteDevice);
        }
      }
      
      return devices;
    }

    public Dictionary<LedId, PinNumber> GetPins(IHardwareInfo hardwareInfo)
    {
      var pins = new Dictionary<LedId, PinNumber>();
      
      foreach(DeviceInfo device in hardwareInfo.Devices)
      {
        foreach (MapInfo mapInfo in device.Map)
        {
          pins.Add(mapInfo.Id, mapInfo.Pin);
        }
      }

      return pins;
    }
  }
}