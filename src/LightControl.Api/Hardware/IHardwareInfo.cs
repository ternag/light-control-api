using System.Collections.Generic;
using LightControl.Api.Infrastructure.Hardware;

namespace LightControl.Api.Hardware
{
  public interface IHardwareInfo
  {
    IEnumerable<DeviceInfo> Devices { get; set; }
  }
}