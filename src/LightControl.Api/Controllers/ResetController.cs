using LightControl.Api.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LightControl.Api.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class ResetController : Controller
  {
    private readonly IHardwareContext _hardwareContext;

    public ResetController(IHardwareContext hardwareContext)
    {
      _hardwareContext = hardwareContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
      _hardwareContext.ReloadHardwareConfiguration();
      return Ok();
    }
  }
}