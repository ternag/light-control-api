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

        public LedController(ILedContext ledContext, ILogger<LedController> logger)
        {
            _logger = logger;
            _ledContext = ledContext;
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

        [HttpGet]
        [Route("{id}/_flick")]
        public ActionResult<Led> Flick(int id)
        {
            _logger.LogInformation($"Flicking LED {id}");

            return CatchExceptions<Led>(() => _ledContext.Flick(id));
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
