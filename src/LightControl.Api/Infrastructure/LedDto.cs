using LightControl.Api.AppModel;

namespace LightControl.Api.Infrastructure
{
  public class LedDto
  {
    public ushort Id { get; }
    public string Display { get; }
    public LedState State { get; }

    public LedDto(in ushort ledId, string ledDisplay, LedState ledState)
    {
      Id = ledId;
      Display = ledDisplay;
      State = ledState;
    }
  }
}