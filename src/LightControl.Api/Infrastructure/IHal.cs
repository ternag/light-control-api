using System;
using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure
{
  // ToDo: Implement IDisposable
  public interface IHal : IDisposable
  {
    void Update(Led led);
  }
}