using Microsoft.AspNetCore.Mvc;

namespace LightControl.Api.Controllers
{
  public class ErrorController : Controller
  {
    [Route("/error")]
    public IActionResult Error() => Problem();
  }
}