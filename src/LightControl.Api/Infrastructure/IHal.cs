using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure
{
  public interface IHal
  {
    void Update(Led led);
  }
}