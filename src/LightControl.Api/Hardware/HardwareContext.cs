using LightControl.Api.Domain;
using LightControl.Api.Hardware.Configuration;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware
{
  public class HardwareContext : IHardwareContext
  {
    private readonly IHardwareConfigurationFactory _hardwareConfigurationFactory;
    private readonly ILogger _logger;

    public HardwareContext(IHardwareConfigurationFactory hardwareConfigurationFactory, ILogger logger)
    {
      _hardwareConfigurationFactory = hardwareConfigurationFactory;
      _logger = logger;
      ReloadHardwareConfiguration();
    }

    public void ReloadHardwareConfiguration()
    {
      _logger.LogInformation("Reload hardware configuration");
      // Dispose old _hal before creating new one
      Hal?.Dispose();
      Hal = new Hal(_hardwareConfigurationFactory.Create());
    }

    public IHal Hal { get; private set; }
  }
}