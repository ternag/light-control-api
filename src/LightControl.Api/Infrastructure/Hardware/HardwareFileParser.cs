using System.IO;
using System.Text.Json;
using LightControl.Api.Hardware;

namespace LightControl.Api.Infrastructure.Hardware
{
  public class HardwareFileParser
  {
    private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
    {
      IgnoreNullValues = true,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      WriteIndented = true
    };

    public HardwareInfo Parse(FileInfo jsonFile)
    {
      var jsonString = File.ReadAllText(jsonFile.FullName);
      var deviceInfos = JsonSerializer.Deserialize<HardwareInfo>(jsonString, SerializerOptions);
      return deviceInfos;
    }
  }
}