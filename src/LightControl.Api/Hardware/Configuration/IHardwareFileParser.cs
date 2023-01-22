using LightControl.Api.Hardware.ConfigurationTransferModel;

namespace LightControl.Api.Hardware.Configuration;

public interface IHardwareFileParser
{
    HardwareInfo Parse(FileInfo jsonFile);
}