﻿using LightControl.Api.Domain;
using LightControl.Api.Hardware.ConfigurationTransferModel;

namespace LightControl.Api.Hardware.Configuration
{
  public interface IHardwareDeviceFactory
  {
    IDevice Create(DeviceInfo device);
  }
}