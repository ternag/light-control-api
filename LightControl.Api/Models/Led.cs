
namespace LightControl.Api.Models
{
    public class Led {

      public Led(int id, string display, LedState state)
      {
        Id = id;
        Display = display;
        State = state;
      }

      public int Id { get; }
      public string Display { get; }
      public LedState State { get; private set; }

      public void Flick()
      {
        if(State == LedState.On)
          State = LedState.Off;
        else
          State = LedState.On;
      }
    }
}