using System.Text.Json;

namespace LightControl.Api.Infrastructure;

public class LightConfigFileParser
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        IgnoreNullValues = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IgnoreReadOnlyProperties = true,
        WriteIndented = true
    };

    private readonly ILogger<LightConfigFileParser> _logger;

    public LightConfigFileParser(ILogger<LightConfigFileParser> logger)
    {
        _logger = logger;
    }

    public LightConfigDto Parse(FileInfo jsonFile)
    {
        _logger.LogInformation($"Parsing light configuration file: '{jsonFile.FullName}'");
        try
        {
            var jsonString = File.ReadAllText(jsonFile.FullName);
            return JsonSerializer.Deserialize<LightConfigDto>(jsonString, SerializerOptions);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error parsing file: '{jsonFile.FullName}'");
            throw;
        }
    }
}