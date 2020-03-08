using System;
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
        public ActionResult<IEnumerable<Led>> Get()
        {
            return CatchExceptions<IEnumerable<Led>>(() => _ledContext.All);
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<Led> Get(int id)
        {
            _logger.LogInformation($"Getting LED {id}");

            return CatchExceptions<Led>(() => _ledContext.Get(id));
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Led> Put(int id, [FromBody]LedUpdateDisplay newDisplayValue)
        {
            _logger.LogInformation($"Updating LED {id}, display={newDisplayValue.Display}");

            return CatchExceptions<Led>(() => {
                    Led knownLed = _ledContext.Get(id);
                    knownLed.Display = newDisplayValue.Display;
                    return knownLed;
                });
        }

        [HttpGet]
        [Route("{id}/_flick")]
        public ActionResult<Led> Flick(int id)
        {
            _logger.LogInformation($"Flicking LED {id}");

            return CatchExceptions<Led>(() => FlickAndUpdate(id));
        }

        private Led FlickAndUpdate(int id)
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
