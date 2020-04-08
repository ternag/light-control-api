using System.Collections.Generic;

namespace LightControl.Api.Infrastructure.Hardware
{
  public class DeviceInfo
  {
    public string DeviceType { get; set; }
    public string BusId { get; set; }
    public string DeviceId { get; set; }
    public List<MapInfo> Map { get; set; }
  }
}