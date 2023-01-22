using Microsoft.AspNetCore.Mvc;

namespace LightControl.Api.Controllers
{
  [ApiController]
  public class HomeController : ControllerBase
  {
    [HttpGet]
    [Route("/")]
    public ActionResult<AppInfo> Get()
    {
      return new AppInfo();
    }
  }
}
