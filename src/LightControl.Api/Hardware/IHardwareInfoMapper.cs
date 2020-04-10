using System.Collections.Generic;
using LightControl.Api.Models;

namespace LightControl.Api.Hardware
{
  public interface IHardwareInfoMapper
  {
    Dictionary<LedId, IDevice> GetDevices(IHardwareInfo hardwareInfo);
    Dictionary<LedId, PinNumber> GetPins(IHardwareInfo hardwareInfo);
  }
}