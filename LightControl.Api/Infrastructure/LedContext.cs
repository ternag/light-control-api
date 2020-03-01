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
        _leds = new List<Led> {
          new Led(0, "Nr. 1", LedState.Off),
          new Led(1, "Nr. 2", LedState.Off),
          new Led(2, "Nr. 3", LedState.Off),
          new Led(3, "Nr. 4", LedState.Off),
          new Led(4, "Nr. 5", LedState.Off),
          new Led(5, "Nr. 6", LedState.Off),
          new Led(6, "Nr. 7", LedState.Off),
          new Led(7, "Nr. 8", LedState.Off),
        };
    }

    public IEnumerable<Led> All => _leds;


    public Led Get(int id)
    {
      if(id < 0 || id > 7) 
      {
        throw new ArgumentException($"{nameof(id)}={id} is out of bounds. Boundry is [0..{_leds.Count - 1}]", nameof(id));
      }

      return _leds.SingleOrDefault(x => x.Id == id);
    }

    public Led Flick(int id)
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