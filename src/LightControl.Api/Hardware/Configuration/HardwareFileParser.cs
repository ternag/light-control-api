using System.Text.Json;
using LightControl.Api.Hardware.ConfigurationTransferModel;

namespace LightControl.Api.Hardware.Configuration;

public class HardwareFileParser : IHardwareFileParser
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        IgnoreNullValues = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IgnoreReadOnlyProperties = true,
        WriteIndented = true
    };

    private readonly ILogger<HardwareFileParser> _logger;

    public HardwareFileParser(ILogger<HardwareFileParser> logger)
    {
        _logger = logger;
    }

    public HardwareInfo Parse(FileInfo jsonFile)
    {
        _logger.LogInformation($"Parsing hardware configuration file: '{jsonFile.FullName}'");
        try
        {
            var jsonString = File.ReadAllText(jsonFile.FullName);
            var deviceInfos = JsonSerializer.Deserialize<HardwareInfo>(jsonString, SerializerOptions);
            return deviceInfos;
        }
        catch (Exception e)
        {
            _logger.LogError(new EventId(1), e, $"Error parsing file: '{jsonFile.FullName}'");
            throw;
        }
    }
}