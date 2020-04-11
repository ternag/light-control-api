using System.Collections.Generic;
using LightControl.Api.Domain;
using LightControl.Api.Hardware.ConfigurationTransferModel;

namespace LightControl.Api.Hardware.Configuration
{
  public interface IHardwareInfoMapper
  {
    Dictionary<LedId, Pin> GetPins(IHardwareInfo hardwareInfo);
  }
}