using System.Collections.Generic;
using LightControl.Api.Hardware.Device;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LightControl.Api.Hardware
{
  
  public class NoHardwareConfigurationFactory : IHardwareConfigurationFactory
  {
    private readonly ILogger _logger;
    private readonly IHardwareConfigurationLoader _hardwareConfigurationLoader;
    private readonly IOptionsMonitor<HardwareOptions> _options;

    public NoHardwareConfigurationFactory(ILogger logger, IHardwareConfigurationLoader hardwareConfigurationLoader, IOptionsMonitor<HardwareOptions> options)
    {
      _logger = logger;
      _hardwareConfigurationLoader = hardwareConfigurationLoader;
      _options = options;
      Init();
    }
    // TODO: Parse json file and create dictionaries

    private void Init()
    {
      // TODO: Parse json file and create dictionaries
      
      // TODO: Fix this initialization stuff
      var devices = GetDevices();
      var pins = GetPins();
      foreach (var deviceInfo in devices)
      {
        LedId id = deviceInfo.Key;
        IDevice device = deviceInfo.Value;
        device.InitPin(pins[id]);
      }
    }

    
    public Dictionary<LedId, IDevice> GetDevices()
    {
      var devices = new Dictionary<LedId, IDevice>();
      var device1 = new NoHardwareDevice(_logger);
      var device2 = new NoHardwareDevice(_logger);
      // Gpio
      devices.Add(0, device1);
      devices.Add(1, device1);
      devices.Add(2, device1);
      devices.Add(3, device1);
      devices.Add(4, device1);
      devices.Add(5, device1);
      devices.Add(6, device1);
      devices.Add(7, device1);
      // mcp23017
      devices.Add(8, device2);
      devices.Add(9, device2);
      devices.Add(10, device2);
      devices.Add(11, device2);
      devices.Add(12, device2);
      devices.Add(13, device2);
      devices.Add(14, device2);
      devices.Add(15, device2);
      devices.Add(16, device2);
      devices.Add(17, device2);
      devices.Add(18, device2);
      devices.Add(19, device2);
      devices.Add(20, device2);
      devices.Add(21, device2);
      devices.Add(22, device2);
      devices.Add(23, device2);

      return devices;
    }

    public Dictionary<LedId, PinNumber> GetPins()
    {
      var pins = new Dictionary<LedId, PinNumber>
      {
        {0, 4},
        {1, 17},
        {2, 27},
        {3, 22},
        {4, 5},
        {5, 6},
        {6, 13},
        {7, 19},
        {8, 0},
        {9, 1},
        {10, 2},
        {11, 3},
        {12, 4},
        {13, 5},
        {14, 6},
        {15, 7},
        {16, 8},
        {17, 9},
        {18, 10},
        {19, 11},
        {20, 12},
        {21, 13},
        {22, 14},
        {23, 15},
      };
      return pins;
    }

    public IHardwareConfiguration Create()
    {
      _logger.LogInformation($"Hardware config filepath '{_options.CurrentValue.ConfigurationFilepath}'");
      return new HardwareConfiguration(GetDevices(), GetPins());
    }
  }
}