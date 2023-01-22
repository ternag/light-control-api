using LightControl.Api.AppModel;
using Microsoft.AspNetCore.Mvc;

namespace LightControl.Api.Controllers;

[ApiController]
public class ResetController : ControllerBase
{
    private readonly IHardwareContext _hardwareContext;

    public ResetController(IHardwareContext hardwareContext)
    {
        _hardwareContext = hardwareContext;
    }

    [HttpGet]
    [Route("/api/reset")]
    public IActionResult Index()
    {
        _hardwareContext.ReloadHardwareConfiguration();
        // TODO: Also reset LedContext
        return Ok();
    }
}