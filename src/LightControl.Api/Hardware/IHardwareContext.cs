namespace LightControl.Api.Hardware
{
  public interface IHardwareContext
  {
    IHal Hal { get; }
    void ReloadHardwareConfiguration();
  }
}