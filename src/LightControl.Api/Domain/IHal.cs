using System;

namespace LightControl.Api.Domain
{
  public interface IHal : IDisposable
  {
    void Update(Led led);
  }
}