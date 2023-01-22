using LightControl.Api.AppModel;
using LightControl.Api.Hardware.ConfigurationTransferModel;
using LightControl.Api.Hardware.Device;

namespace LightControl.Api.Hardware.Configuration;

public class HardwareDeviceFactory : IHardwareDeviceFactory
{
    private readonly ILogger _logger;

    public HardwareDeviceFactory(ILogger logger)
    {
        _logger = logger;
    }

    public IDevice Create(DeviceInfo device)
    {
        if (device == null)
            throw new ArgumentNullException(nameof(device));

        return device.DeviceType.ToLower() switch
        {
            DeviceType.DummyDevice => new DummyHardwareDevice(_logger),
            DeviceType.Gpio => new GpioDevice(_logger),
            DeviceType.Mcp23017 => new Mcp23017(new Mcp23017Address(device.DeviceIdAsUShort), device.BusIdAsUShort, _logger),
            DeviceType.Pca9685 => new Pca9685Device(device.BusIdAsUShort, device.DeviceIdAsUShort, _logger),
            _ => throw new Exception($"Error creating device: Device '{device.DeviceType}' is unknown")
        };
    }
}
