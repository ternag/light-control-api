using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Controllers
{
  [ApiController]
  [Route("/")]
  public class HomeController : ControllerBase
  {
    private readonly ILogger _logger;

    public HomeController(ILogger logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public ActionResult<AppInfo> Get()
    {
      return new AppInfo();
    }
  }
}