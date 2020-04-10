using LightControl.Api.Infrastructure.Hardware;
using LightControl.Api.Utils;

namespace LightControl.Api.Hardware
{
  public interface IHardwareDeviceFactory
  {
    IDevice Create(DeviceInfo device);
  }
}