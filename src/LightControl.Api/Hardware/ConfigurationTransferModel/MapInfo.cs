﻿namespace LightControl.Api.Hardware.ConfigurationTransferModel;

public class MapInfo
{
    public MapInfo()
    {
    }

    public MapInfo(string id, int pin)
    {
        Id = id;
        Pin = pin;
    }

    public required string Id { get; set; } // TODO: rename to ledId
    public int Pin { get; set; } // TODO: rename to PinNumber
}