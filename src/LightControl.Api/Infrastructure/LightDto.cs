using LightControl.Api.AppModel;

namespace LightControl.Api.Infrastructure
{
  public class LightDto
  {
    public LedId LedId { get; set; }
    public string Name { get; set; }
  }
}