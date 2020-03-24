using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LightControl.Api.Models;
using LightControl.Api.Infrastructure;

namespace LightControl.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LedController : ControllerBase
    {
        private readonly ILogger<LedController> _logger;
        private readonly ILedContext _ledContext;
        private readonly IHal _hal;

        public LedController(ILedContext ledContext, ILogger<LedController> logger, IHal hal)
        {
            _logger = logger;
            _ledContext = ledContext;
            _hal = hal;
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
            // TODO: Create dto for the Led class
            return CatchExceptions(() => _ledContext.Get(id).ToDto());
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<LedDto> Put(ushort id, [FromBody]LedUpdateDisplay newDisplayValue)
        {
            _logger.LogInformation($"Updating LED {id}, display={newDisplayValue.Display}");

            return CatchExceptions(() => {
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
            _hal.Update(led);
            return led;
        }

        private ActionResult<T> CatchExceptions<T>(Func<T> method)
        {
            try
            {
                return (T)method();
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
