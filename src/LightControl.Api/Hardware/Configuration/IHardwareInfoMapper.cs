﻿using System.Collections.Generic;
using LightControl.Api.Domain;

namespace LightControl.Api.Hardware.Configuration
{
  public interface IHardwareInfoMapper
  {
    Dictionary<LedId, IDevice> GetDevices(IHardwareInfo hardwareInfo);
    Dictionary<LedId, PinNumber> GetPins(IHardwareInfo hardwareInfo);
  }
}