using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Gpio;
using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure 
{
  public static class HardwareExtensions 
  {
    /// <summery>
    /// Map the LedState from the application domain to the PinValue in the hardware domain
    /// </summery>
    public static PinValue ToPinValue(this LedState state)
    {
      if(state == LedState.Off)
      {
        return PinValue.Low;
      }
      return PinValue.High;
    }
  }
}