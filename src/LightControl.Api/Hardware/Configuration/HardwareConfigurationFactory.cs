﻿using LightControl.Api.AppModel;
using Microsoft.Extensions.Options;

namespace LightControl.Api.Hardware.Configuration;

public class HardwareConfigurationFactory : IHardwareConfigurationFactory
{
    private readonly IHardwareFileParser _fileParser;
    private readonly ILogger _logger;
    private readonly IHardwareInfoMapper _mapper;
    private readonly HardwareOptions _options;
    private Dictionary<LedId, Pin> _pins = null!;

    public HardwareConfigurationFactory(ILogger<HardwareConfigurationFactory> logger,
        IOptions<HardwareOptions> options,
        IHardwareFileParser fileParser,
        IHardwareInfoMapper mapper)
    {
        _logger = logger;
        _options = options.Value;
        _fileParser = fileParser;
        _mapper = mapper;
        Init();
    }

    public IHardwareConfiguration Create()
    {
        _logger.LogInformation($"Hardware config filepath {_options.ConfigurationFilepath}");
        Init();
        return new HardwareConfiguration(_pins);
    }

    private void Init()
    {
        var configurationFilepath = new FileInfo(_options.ConfigurationFilepath);
        var hardwareInfo = _fileParser.Parse(configurationFilepath);
        _pins = _mapper.GetPins(hardwareInfo);
        foreach (var (_, pin) in _pins) pin.Init();
    }
}
