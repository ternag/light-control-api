﻿using LightControl.Api.Hardware.Configuration;

namespace LightControl.Api.Hardware.ConfigurationTransferModel;

public class HardwareInfo : IHardwareInfo
{
    public required IEnumerable<DeviceInfo> Devices { get; set; }
}