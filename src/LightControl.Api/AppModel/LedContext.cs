using System;
using System.Collections.Generic;
using System.Linq;
using LightControl.Api.Infrastructure;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.AppModel
{
  public class LedContext : ILedContext
  {
    private readonly ILogger _logger;

    public LedContext(ILogger logger)
    {
      _logger = logger;
      // TODO: Inject the list 
      _leds = new Dictionary<LedId, Led>
      {
        {0,new Led(0, "Nr. 1", LedState.Off)},
        {1,new Led(1, "Nr. 2", LedState.Off)},
        {2,new Led(2, "Nr. 3", LedState.Off)},
        {3,new Led(3, "Nr. 4", LedState.Off)},
        {4,new Led(4, "Nr. 5", LedState.Off)},
        {5,new Led(5, "Nr. 6", LedState.Off)},
        {6,new Led(6, "Nr. 7", LedState.Off)},
        {7,new Led(7, "Nr. 8", LedState.Off)},
        {0x10, new Led(0x10, "Nr. 1, mcp23017", LedState.Off)},
        {0x11, new Led(0x11, "Nr. 2, mcp23017", LedState.Off)},
        {0x12, new Led(0x12, "Nr. 3, mcp23017", LedState.Off)},
        {0x13, new Led(0x13, "Nr. 4, mcp23017", LedState.Off)},
        {0x14, new Led(0x14, "Nr. 5, mcp23017", LedState.Off)},
        {0x15, new Led(0x15, "Nr. 6, mcp23017", LedState.Off)},
        {0x16, new Led(0x16, "Nr. 7, mcp23017", LedState.Off)},
        {0x17, new Led(0x17, "Nr. 8, mcp23017", LedState.Off)},
        {0x18, new Led(0x18, "Nr. 9, mcp23017", LedState.Off)},
        {0x19, new Led(0x19, "Nr. 10, mcp23017", LedState.Off)},
        {0x1a, new Led(0x1a, "Nr. 11, mcp23017", LedState.Off)},
        {0x1b, new Led(0x1b, "Nr. 12, mcp23017", LedState.Off)},
        {0x1c, new Led(0x1c, "Nr. 13, mcp23017", LedState.Off)},
        {0x1d, new Led(0x1d, "Nr. 14, mcp23017", LedState.Off)},
        {0x1e, new Led(0x1e, "Nr. 15, mcp23017", LedState.Off)},
        {0x1f, new Led(0x1f, "Nr. 16, mcp23017", LedState.Off)},
      };
    }

    public IEnumerable<Led> All => _leds.Values;
    public LightConfigDto Config { get; }

    private IDictionary<LedId, Led> _leds { get; }
    public Led Get(LedId ledId)
    {
      if (_leds.ContainsKey(ledId))
      {
        return _leds[ledId];
      }
      else
      {
        throw new ArgumentException($"{nameof(ledId)}={ledId} is unknown");
      }
    }

    public Led Flick(LedId id)
    {
      Led led = Get(id);
      led?.Flick();

      return led;
    }

    public LightGroupDto FlickGroup(LightGroupId id)
    {
      throw new NotImplementedException();
    }
  }
}