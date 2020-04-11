using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightControl.Api.Domain;
using LightControl.Api.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LightControl.Api.Infrastructure;

namespace LightControl.Api.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class LedController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly ILedContext _ledContext;
    private readonly IHardwareContext _hardwareContext;

    public LedController(ILedContext ledContext, ILogger logger, IHardwareContext hardwareContext)
    {
      _logger = logger;
      _ledContext = ledContext;
      _hardwareContext = hardwareContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<LedDto>> Get()
    {
      return CatchExceptions(() => _ledContext.All.Select(l => l.ToDto()));
    }


    [HttpGet]
    [Route("{id}")]
    public ActionResult<LedDto> Get(ushort id)
    {
      _logger.LogInformation($"Getting LED {id}");
      return CatchExceptions(() => _ledContext.Get(id).ToDto());
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult<LedDto> Put(ushort id, [FromBody] LedUpdateDisplay newDisplayValue)
    {
      _logger.LogInformation($"Updating LED {id}, display={newDisplayValue.Display}");

      return CatchExceptions(() =>
      {
        Led knownLed = _ledContext.Get(id);
        knownLed.Display = newDisplayValue.Display;
        return knownLed.ToDto();
      });
    }

    [HttpGet]
    [Route("{id}/_flick")]
    public ActionResult<LedDto> Flick(ushort id)
    {
      _logger.LogInformation($"Flicking LED {id}");
      return CatchExceptions(() => FlickAndUpdate(id).ToDto());
    }

    private Led FlickAndUpdate(ushort id)
    {
      var led = _ledContext.Flick(id);
      _hardwareContext.Hal.Update(led);
      return led;
    }

    private ActionResult<T> CatchExceptions<T>(Func<T> method)
    {
      try
      {
        return method();
      }
      catch (ArgumentException ae)
      {
        return NotFound(ae.Message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}