using System;
using LightControl.Api.Domain;

namespace LightControl.Api.Hardware.Configuration
{
  public interface IHardwareConfiguration : IDisposable
  {
    Pin GetPin(LedId id);
  }
}