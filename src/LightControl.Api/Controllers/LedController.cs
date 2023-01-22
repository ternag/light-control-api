using LightControl.Api.AppModel;
using LightControl.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace LightControl.Api.Controllers;

[ApiController]
public class LedController : ControllerBase
{
    private readonly IHardwareContext _hardwareContext;
    private readonly ILedContext _ledContext;
    private readonly ILogger _logger;

    public LedController(ILedContext ledContext, ILogger logger, IHardwareContext hardwareContext)
    {
        _logger = logger;
        _ledContext = ledContext;
        _hardwareContext = hardwareContext;
    }

    [HttpGet]
    [Route("/api/led")]
    public ActionResult<IEnumerable<LedDto>> Get()
    {
        return CatchExceptions(() => _ledContext.All.Select(l => l.ToDto()));
    }


    [HttpGet]
    [Route("/api/led/{id}")]
    public ActionResult<LedDto> Get(ushort id)
    {
        _logger.LogInformation($"Getting LED {id}");
        return CatchExceptions(() => _ledContext.Get(id).ToDto());
    }

    [HttpPut]
    [Route("/api/led/{id}")]
    public ActionResult<LedDto> Put(ushort id, [FromBody] LedUpdateDisplay newDisplayValue)
    {
        _logger.LogInformation($"Updating LED {id}, display={newDisplayValue.Display}");

        return CatchExceptions(() =>
        {
            var knownLed = _ledContext.Get(id);
            knownLed.Display = newDisplayValue.Display;
            return knownLed.ToDto();
        });
    }

    [HttpGet]
    [Route("/api/led/{id}/_flick")]
    public ActionResult<LedDto> Flick(ushort id)
    {
        _logger.LogInformation($"Flicking LED {id}");
        return CatchExceptions(() => FlickAndUpdate(id).ToDto());
    }

    private Led FlickAndUpdate(LedId id)
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
            _logger.LogError(ae, ae.Message);
            return NotFound(ae.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest(e.Message);
        }
    }
}