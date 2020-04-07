using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LightControl.Api.Hardware;

namespace LightControl.Api.Infrastructure.Hardware
{
  public class HardwareFileParser : IHardwareFileParser
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

    public void Serialize()
    {
      HardwareInfo hardwareInfo = new HardwareInfo()
      {
        Devices = new List<DeviceInfo>
        {
          new DeviceInfo
          {
            DeviceType = DeviceType.Gpio,
            Map = new List<MapInfo>
            {
              new MapInfo {Id = "0x0", Pin = 4},
              new MapInfo {Id = "0x1", Pin = 17},
              new MapInfo {Id = "0x2", Pin = 27}
            }
          },
          new DeviceInfo
          {
            DeviceType = DeviceType.Mcp23017,
            BusId = "1",
            DeviceId = "0x20",
            Map = new List<MapInfo>()
            {
              new MapInfo {Id = "0x10", Pin = 0},
              new MapInfo {Id = "0x11", Pin = 1},
              new MapInfo {Id = "0x12", Pin = 2}
            }
          }
        }
      };
      
      JsonSerializerOptions options = new JsonSerializerOptions();
      options.IgnoreNullValues = true;
      options.WriteIndented = true;
      options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      string hwJson = JsonSerializer.Serialize(hardwareInfo, options);
      File.WriteAllText("./hardware/hw.json", hwJson);
    }
  }

  public static class DeviceType
  {
    public static readonly string Gpio = "GPIO";
    public static readonly string Mcp23017 = "MCP23017";
  }

  public class HardwareInfo
  {
    public IEnumerable<DeviceInfo> Devices { get; set; }
  }
  
  public class DeviceInfo
  {
    public string DeviceType { get; set; }
    public string BusId { get; set; }
    public string DeviceId { get; set; }
    public List<MapInfo> Map { get; set; }
  }

  public class MapInfo
  {
    public string Id { get; set; }
    public int Pin { get; set; }
  }
}