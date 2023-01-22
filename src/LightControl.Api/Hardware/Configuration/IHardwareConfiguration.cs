using LightControl.Api.AppModel;

namespace LightControl.Api.Hardware.Configuration;

public interface IHardwareConfiguration : IDisposable
{
    Pin GetPin(LedId id);
}