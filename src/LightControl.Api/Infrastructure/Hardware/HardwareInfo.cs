using System.Collections.Generic;
using LightControl.Api.Hardware;

namespace LightControl.Api.Infrastructure.Hardware
{
  public class HardwareInfo : IHardwareInfo
  {
    public IEnumerable<DeviceInfo> Devices { get; set; }
  }
}