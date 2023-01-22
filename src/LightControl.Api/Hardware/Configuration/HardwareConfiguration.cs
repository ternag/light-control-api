using LightControl.Api.AppModel;

namespace LightControl.Api.Hardware.Configuration;

public class HardwareConfiguration : IHardwareConfiguration
{
    private readonly Dictionary<LedId, Pin> _pins;

    public HardwareConfiguration(Dictionary<LedId, Pin> pins)
    {
        _pins = pins ?? throw new ArgumentNullException(nameof(pins));
    }

    public Pin GetPin(LedId id)
    {
        if (_pins.ContainsKey(id))
            return _pins[id];
        throw new ArgumentException(
            $"The LedId '{id}' is unknown. Make sure the id is registered in the hardware configuration");
    }

    public void Dispose()
    {
        foreach (var pair in _pins) pair.Value?.Device?.Dispose();
    }
}