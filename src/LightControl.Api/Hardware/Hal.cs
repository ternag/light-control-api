using LightControl.Api.AppModel;
using LightControl.Api.Hardware.Configuration;

namespace LightControl.Api.Hardware;

public class Hal : IHal
{
    private readonly IHardwareConfiguration _hardwareConfiguration;

    public Hal(IHardwareConfiguration hardwareConfiguration)
    {
        _hardwareConfiguration = hardwareConfiguration;
    }

    public void Update(Led led)
    {
        var pin = _hardwareConfiguration.GetPin(led.Id);
        pin.Device.Write(pin.PinNumber, led.State); // ToDo: Create SetState method on Pin and hide Device
    }

    public void Dispose()
    {
        _hardwareConfiguration?.Dispose();
    }
}