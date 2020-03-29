using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure
{
  // ToDo: Implement IDisposable
  public interface IHal
  {
    void Update(Led led);
  }
}