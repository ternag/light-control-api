using System.Collections.Generic;
using System.IO;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LightControl.Api.Hardware.Configuration
{
  public class HardwareConfigurationFactory : IHardwareConfigurationFactory
  {
    private readonly ILogger _logger;
    private readonly IOptions<HardwareOptions> _options;
    private readonly IHardwareFileParser _fileParser;
    private readonly IHardwareInfoMapper _mapper;
    private Dictionary<LedId, IDevice> _devices;
    private Dictionary<LedId, PinNumber> _pins;

    public HardwareConfigurationFactory(ILogger<HardwareConfigurationFactory> logger, IOptions<HardwareOptions> options, IHardwareFileParser fileParser, IHardwareInfoMapper mapper)
    {
      _logger = logger;
      _options = options;
      _fileParser = fileParser;
      _mapper = mapper;
      Init();
    }

    private void Init()
    {
      var configurationFilepath = new FileInfo(_options.Value.ConfigurationFilepath);
      var hardwareInfo = _fileParser.Parse(configurationFilepath);
      _devices = _mapper.GetDevices(hardwareInfo);
      _pins = _mapper.GetPins(hardwareInfo);
      // TODO: Init Pins on devices
    }

    public IHardwareConfiguration Create()
    {
      _logger.LogInformation($"Hardware config filepath {_options.Value.ConfigurationFilepath}");
      Init();
      return new HardwareConfiguration(_devices, _pins);
    }
  }
}