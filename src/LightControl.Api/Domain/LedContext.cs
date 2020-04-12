using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Domain
{
  public class LedContext : ILedContext
  {
    private readonly List<Led> _leds;
    private readonly ILogger _logger;

    public LedContext(ILogger logger)
    {
      _logger = logger;
      // TODO: Inject the list 
      _leds = new List<Led>
      {
        new Led(0, "Nr. 1", LedState.Off),
        new Led(1, "Nr. 2", LedState.Off),
        new Led(2, "Nr. 3", LedState.Off),
        new Led(3, "Nr. 4", LedState.Off),
        new Led(4, "Nr. 5", LedState.Off),
        new Led(5, "Nr. 6", LedState.Off),
        new Led(6, "Nr. 7", LedState.Off),
        new Led(7, "Nr. 8", LedState.Off),
        new Led(0x10, "Nr. 1, mcp23017", LedState.Off),
        new Led(0x11, "Nr. 2, mcp23017", LedState.Off),
        new Led(0x12, "Nr. 3, mcp23017", LedState.Off),
        new Led(0x13, "Nr. 4, mcp23017", LedState.Off),
        new Led(0x14, "Nr. 5, mcp23017", LedState.Off),
        new Led(0x15, "Nr. 6, mcp23017", LedState.Off),
        new Led(0x16, "Nr. 7, mcp23017", LedState.Off),
        new Led(0x17, "Nr. 8, mcp23017", LedState.Off),
        new Led(0x18, "Nr. 9, mcp23017", LedState.Off),
        new Led(0x19, "Nr. 10, mcp23017", LedState.Off),
        new Led(0x1a, "Nr. 11, mcp23017", LedState.Off),
        new Led(0x1b, "Nr. 12, mcp23017", LedState.Off),
        new Led(0x1c, "Nr. 13, mcp23017", LedState.Off),
        new Led(0x1d, "Nr. 14, mcp23017", LedState.Off),
        new Led(0x1e, "Nr. 15, mcp23017", LedState.Off),
        new Led(0x1f, "Nr. 16, mcp23017", LedState.Off),
      };
    }

    public IEnumerable<Led> All => _leds;


    public Led Get(LedId ledId)
    {
      if (ledId < 0 || ledId > (_leds.Count - 1))
      {
        throw new ArgumentException($"{nameof(ledId)}={ledId} is out of bounds. Boundary is [0..{_leds.Count - 1}]",
          nameof(ledId));
      }

      return _leds.SingleOrDefault(x => x.Id == ledId);
    }

    public Led Flick(LedId id)
    {
      Led led = Get(id);
      led?.Flick();

      return led;
    }
  }
}