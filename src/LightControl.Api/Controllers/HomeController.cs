using LightControl.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Controllers
{
  [ApiController]
  [Route("/")]
  public class HomeController : ControllerBase
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public ActionResult<Info> Get()
    {
      return new Info();
    }
  }
}