namespace LightControl.Api.Domain
{
  public class Led
  {
    public Led(LedId id, string display, LedState state)
    {
      Id = id;
      Display = display;
      State = state;
    }

    public LedId Id { get; }
    public string Display { get; set; }
    public LedState State { get; private set; }

    public void Flick()
    {
      if (State == LedState.On)
        State = LedState.Off;
      else
        State = LedState.On;
    }
  }
}