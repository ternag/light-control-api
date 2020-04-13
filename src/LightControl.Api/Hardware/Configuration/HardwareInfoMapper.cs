using System.Collections.Generic;
using LightControl.Api.AppModel;
using LightControl.Api.Hardware.ConfigurationTransferModel;

namespace LightControl.Api.Hardware.Configuration
{
  public class HardwareInfoMapper : IHardwareInfoMapper
  {
    private readonly IHardwareDeviceFactory _hardwareDeviceFactory;

    public HardwareInfoMapper(IHardwareDeviceFactory hardwareDeviceFactory)
    {
      _hardwareDeviceFactory = hardwareDeviceFactory;
    }

    public Dictionary<LedId, Pin> GetPins(IHardwareInfo hardwareInfo)
    {
      var pins = new Dictionary<LedId, Pin>();
      
      foreach(DeviceInfo device in hardwareInfo.Devices)
      {
        IDevice concreteDevice = _hardwareDeviceFactory.Create(device);

        foreach (MapInfo mapInfo in device.Map)
        {
          pins.Add(mapInfo.Id, new Pin(mapInfo.Pin, concreteDevice));
        }
      }

      return pins;
    }
  }
}