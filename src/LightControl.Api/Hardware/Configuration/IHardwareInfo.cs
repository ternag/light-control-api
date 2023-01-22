using LightControl.Api.Hardware.ConfigurationTransferModel;

namespace LightControl.Api.Hardware.Configuration;

public interface IHardwareInfo
{
    IEnumerable<DeviceInfo> Devices { get; set; }
}