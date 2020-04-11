using LightControl.Api.Domain;

namespace LightControl.Api.Infrastructure
{
  public static class Extensions
  {
    public static LedDto ToDto(this Led led)
    {
      return new LedDto((ushort) led.Id, led.Display, led.State);
    }
  }
}