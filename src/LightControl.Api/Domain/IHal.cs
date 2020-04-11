using System;

namespace LightControl.Api.Domain
{
  // ToDo: Implement IDisposable
  public interface IHal : IDisposable
  {
    void Update(Led led);
  }
}