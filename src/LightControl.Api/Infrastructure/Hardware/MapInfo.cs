namespace LightControl.Api.Infrastructure.Hardware
{
  public class MapInfo
  {
    public MapInfo()
    {
    }

    public MapInfo(string id, int pin)
    {
      Id = id;
      Pin = pin;
    }

    public string Id { get; set; }
    public int Pin { get; set; }
  }
}