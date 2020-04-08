using System.Collections.Generic;

namespace LightControl.Api.Infrastructure.Hardware
{
  public class HardwareInfo
  {
    public IEnumerable<DeviceInfo> Devices { get; set; }
  }
}