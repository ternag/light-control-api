namespace LightControl.Api.AppModel;

public interface IHardwareContext
{
    IHal Hal { get; }
    void ReloadHardwareConfiguration();
}