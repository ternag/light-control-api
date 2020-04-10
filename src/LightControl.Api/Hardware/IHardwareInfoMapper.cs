using System.Collections.Generic;
using LightControl.Api.Infrastructure.Hardware;
using LightControl.Api.Models;

namespace LightControl.Api.Hardware
{
  public interface IHardwareInfoMapper
  {
    Dictionary<LedId, IDevice> GetDevices(HardwareInfo hardwareInfo);
    Dictionary<LedId, PinNumber> GetPins(HardwareInfo hardwareInfo);
  }
}