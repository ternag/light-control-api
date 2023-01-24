using System.Device.Gpio;
using LightControl.Api.AppModel;

namespace LightControl.Api.Hardware.Extensions;

public static class HardwareExtensions
{
    public static PinValue ToPinValue(this LedState state)
    {
        return state == LedState.Off ? PinValue.Low : PinValue.High;
    }

    public static PinValue ToPinValue(this double state)
    {
        return state == 0.0 ? PinValue.Low : PinValue.High;
    }

    public static bool ToBool(this LedState state)
    {
        return state != LedState.Off;
    }

    public static bool ToBool(this double value)
    {
        return value != 0.0;
    }
}
