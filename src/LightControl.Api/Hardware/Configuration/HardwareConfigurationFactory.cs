using System.Collections.Generic;
using System.IO;
using LightControl.Api.Domain;
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
    private Dictionary<LedId, Pin> _pins;

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
      _pins = _mapper.GetPins(hardwareInfo);
      foreach (var (_, pin) in _pins)
      {
        pin.Init();
      }
    }

    public IHardwareConfiguration Create()
    {
      _logger.LogInformation($"Hardware config filepath {_options.Value.ConfigurationFilepath}");
      Init();
      return new HardwareConfiguration(_pins);
    }
  }
}