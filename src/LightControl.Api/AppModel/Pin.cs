using System;

namespace LightControl.Api.AppModel
{
  public class Pin
  {
    public Pin(PinNumber pinNumber, IDevice device)
    {
      PinNumber = pinNumber;
      Device = device ?? throw new ArgumentNullException(nameof(device));
    }

    public PinNumber PinNumber { get; }
    public IDevice Device { get; }

    public void Init()
    {
      Device.InitPin(PinNumber);
    }
  }
}