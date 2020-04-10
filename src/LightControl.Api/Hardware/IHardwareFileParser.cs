using System.IO;
using LightControl.Api.Infrastructure.Hardware;

namespace LightControl.Api.Hardware
{
  public interface IHardwareFileParser
  {
    HardwareInfo Parse(FileInfo jsonFile);
  }
}