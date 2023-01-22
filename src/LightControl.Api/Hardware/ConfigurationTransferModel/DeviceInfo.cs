using LightControl.Api.Utils;

namespace LightControl.Api.Hardware.ConfigurationTransferModel;

public class DeviceInfo
{
    public string DeviceType { get; set; }
    public string BusId { get; set; }
    public string DeviceId { get; set; }
    public List<MapInfo> Map { get; set; }

    public ushort DeviceIdAsUShort => Convert.ToUInt16(DeviceId, DeviceId.GetBase());
    public ushort BusIdAsUShort => Convert.ToUInt16(BusId, BusId.GetBase());
}