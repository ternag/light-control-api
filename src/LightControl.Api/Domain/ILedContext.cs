using System.Collections.Generic;

namespace LightControl.Api.Domain
{
  public interface ILedContext
  {
    IEnumerable<Led> All { get; }
    Led Get(LedId ledId);
    Led Flick(LedId id);
  }
}