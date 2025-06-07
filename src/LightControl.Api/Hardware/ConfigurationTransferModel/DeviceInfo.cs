using LightControl.Api.Utils;

namespace LightControl.Api.Hardware.ConfigurationTransferModel;

public class DeviceInfo
{
    public required string DeviceType { get; set; }
    public string? BusId { get; set; }
    public string? DeviceId { get; set; }
    public required List<MapInfo> Map { get; set; }

    public ushort DeviceIdAsUShort => DeviceId != null ? Convert.ToUInt16(DeviceId, DeviceId.GetBase()) : (ushort)0;
    public ushort BusIdAsUShort => BusId != null ? Convert.ToUInt16(BusId, BusId.GetBase()) : (ushort)0;
}