using Microsoft.AspNetCore.Mvc;

namespace LightControl.Api.Controllers
{
  public class ErrorController : ControllerBase
  {
    [HttpGet]
    [Route("/error")]
    public IActionResult Error() => Problem();
  }
}
