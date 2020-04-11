namespace LightControl.Api.Domain
{
  public interface IHardwareContext
  {
    IHal Hal { get; }
    void ReloadHardwareConfiguration();
  }
}