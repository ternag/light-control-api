namespace LightControl.Api.Hardware.Configuration;

public class HardwareOptions
{
    public const string SectionName = "HardwareOptions";
    public required string ConfigurationFilepath { get; set; }
}
