using LightControl.Api.Infrastructure;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware
{
  public interface IHardwareContext
  {
    IHal Hal { get; }
    void ReloadHardwareConfiguration();
  }

  public class HardwareContext : IHardwareContext
  {
    private readonly IHardwareConfigurationFactory _hardwareConfigurationFactory;
    private readonly ILogger<HardwareContext> _logger;
    private IHal _hal;

    public HardwareContext(IHardwareConfigurationFactory hardwareConfigurationFactory, ILogger<HardwareContext> logger)
    {
      _hardwareConfigurationFactory = hardwareConfigurationFactory;
      InitHardware();
    }

    private void InitHardware()
    {
      var hardwareConfiguration = _hardwareConfigurationFactory.Create();
      // ToDo: Dispose old _hal before creating new one
      _hal = new Hal(hardwareConfiguration);
    }

    public void ReloadHardwareConfiguration()
    {
      InitHardware();
    }

    public IHal Hal => _hal;
  }
}