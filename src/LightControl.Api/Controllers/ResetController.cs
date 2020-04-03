using System;
using LightControl.Api.Hardware;
using Microsoft.AspNetCore.Mvc;

namespace LightControl.Api.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class ResetController : Controller
  {
    private IHardwareContext _hardwareContext;

    public ResetController(IHardwareContext hardwareContext)
    {
      _hardwareContext = hardwareContext;
    }

    // GET
    public IActionResult Index()
    {
      _hardwareContext.ReloadHardwareConfiguration();
      return Ok();
    }
  }
}