using Microsoft.AspNetCore.Mvc;

namespace LightControl.Api.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class ResetController : Controller
  {
    // GET
    public ActionResult<bool> Index()
    {
      return false;
    }
  }
}