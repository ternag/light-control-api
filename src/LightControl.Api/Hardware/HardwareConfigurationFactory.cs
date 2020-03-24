using System.Collections.Generic;
using LightControl.Api.Hardware.Device;
using LightControl.Api.Infrastructure;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Hardware
{
  // {
  //   "gpio": {
  //     "deviceType": "gpio",
  //     "map": [
  //       { id: 0, pin: 4},
  //       { id: 1, pin: 17},
  //       { id: 2, pin: 27},
  //       { id: 3, pin: 22},
  //       { id: 4, pin: 5},
  //       { id: 5, pin: 6},
  //       { id: 6, pin: 13},
  //       { id: 7, pin: 19}
  //     ]
  //   },
  //   "i2c-1": {
  //     "deviceType": "mcp23017",
  //     "deviceId": 0x20,
  //     "busId": 1,
  //     "map": [
  //       { id: 112, pin: 0},
  //       { id: 113, pin: 1},
  //       { id: 114, pin: 2},
  //       { id: 115, pin: 3},
  //       { id: 116, pin: 4},
  //       { id: 117, pin: 5},
  //       { id: 118, pin: 6},
  //       { id: 119, pin: 7},
  //       { id: 120, pin: 8},
  //       { id: 121, pin: 9},
  //       { id: 122, pin: 10},
  //     ]
  //   }
  // }
  public interface IHardwareConfigurationFactory
  {
    IHardwareConfiguration Create(ILogger<Hal> logger);
  }

  public class HardwareConfigurationFactory : IHardwareConfigurationFactory
  {
    private readonly ILogger<HardwareConfiguration> _logger;

    public HardwareConfigurationFactory(ILogger<HardwareConfiguration> logger)
    {
      _logger = logger;
      Init();
    }

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
        _logger.LogInformation($"Initialized pin {pins[id]} on {device.Name}");
      }
    }
    

    public Dictionary<LedId, IDevice> GetDevices()
    {
      // TODO: devices should not be created every time GetDevices are called. Implement parser of configuration file.
      var devices = new Dictionary<LedId, IDevice>();
      var device1 = new GpioDevice(_logger);
      var device2 = new Mcp23017(new Mcp23017Address(0x20));
      
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

    public IHardwareConfiguration Create(ILogger<Hal> logger)
    {
      return new HardwareConfiguration(GetDevices(), GetPins(), logger);
    }
  }
}