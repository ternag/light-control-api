using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure
{
  public class LedContext : ILedContext
  {
    private readonly List<Led> _leds;
    private readonly ILogger<LedContext> _logger;

    public LedContext(ILogger<LedContext> logger)
    {
      _logger = logger;
      // TODO: Inject the list 
      _leds = new List<Led> {
        new Led(0, "Nr. 1", LedState.Off),
        new Led(1, "Nr. 2", LedState.Off),
        new Led(2, "Nr. 3", LedState.Off),
        new Led(3, "Nr. 4", LedState.Off),
        new Led(4, "Nr. 5", LedState.Off),
        new Led(5, "Nr. 6", LedState.Off),
        new Led(6, "Nr. 7", LedState.Off),
        new Led(7, "Nr. 8", LedState.Off),
        new Led(8, "Nr. 1, mcp23017", LedState.Off),
        new Led(9, "Nr. 2, mcp23017", LedState.Off),
        new Led(10, "Nr. 3, mcp23017", LedState.Off),
        new Led(11, "Nr. 4, mcp23017", LedState.Off),
        new Led(12, "Nr. 5, mcp23017", LedState.Off),
        new Led(13, "Nr. 6, mcp23017", LedState.Off),
        new Led(14, "Nr. 7, mcp23017", LedState.Off),
        new Led(15, "Nr. 8, mcp23017", LedState.Off),
        new Led(16, "Nr. 9, mcp23017", LedState.Off),
        new Led(17, "Nr. 10, mcp23017", LedState.Off),
        new Led(18, "Nr. 11, mcp23017", LedState.Off),
        new Led(19, "Nr. 12, mcp23017", LedState.Off),
        new Led(20, "Nr. 13, mcp23017", LedState.Off),
        new Led(21, "Nr. 14, mcp23017", LedState.Off),
        new Led(22, "Nr. 15, mcp23017", LedState.Off),
        new Led(23, "Nr. 16, mcp23017", LedState.Off),
      };
    }

    public IEnumerable<Led> All => _leds;


    public Led Get(LedId ledId)
    {
      if(ledId < 0 || ledId > (_leds.Count - 1)) 
      {
        throw new ArgumentException($"{nameof(ledId)}={ledId} is out of bounds. Boundry is [0..{_leds.Count - 1}]", nameof(ledId));
      }

      return _leds.SingleOrDefault(x => x.Id == ledId);
    }

    public Led Flick(LedId id)
    {
      Led led = Get(id);
      if(led != null)
      {
        led.Flick();
      }
      return led;
    }
  }
}