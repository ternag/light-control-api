using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Gpio;
using LightControl.Api.Models;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using LightControl.Api.Hardware;
using LightControl.Api.Infrastructure.Device;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Infrastructure
{
  public class HardwareConfiguration 
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
    public HardwareConfiguration(ILogger logger)
    {
      // TODO: Inject via constructor
      LEDs = new List<LED> { 
        new LED(0,4), 
        new LED(1,17), 
        new LED(2,27),
        new LED(3,22),
        new LED(4,5),
        new LED(5,6),
        new LED(6,13),
        new LED(7,19)
      };
      Init(logger);
    }

    private void Init(ILogger logger)
    {
      _devices = new Dictionary<PinId, IDevice>();
      _pins = new Dictionary<PinId, PinNumber>();
      // var device1 = new GpioDevice();
      var device1 = new NoHardwareDevice(logger);
      _devices.Add(0, device1);
      _devices.Add(1, device1);
      _devices.Add(2, device1);
      _devices.Add(3, device1);
      _devices.Add(4, device1);
      _devices.Add(5, device1);
      _devices.Add(6, device1);
      _devices.Add(7, device1);
      
      _pins.Add(0, 4);
      _pins.Add(1, 17);
      _pins.Add(2, 27);
      _pins.Add(3, 22);
      _pins.Add(4, 5);
      _pins.Add(5, 6);
      _pins.Add(6, 13);
      _pins.Add(7, 19);
    }

    private Dictionary<PinId, IDevice> _devices;
    private Dictionary<PinId, PinNumber> _pins;

    public IEnumerable<LED> LEDs { get; }
    
    public IDevice GetDevice(PinId id)
    {
      if (_devices.ContainsKey(id))
        return _devices[id];
      else
        throw new ArgumentException($"The Pin id '{id}' is unknown. Make sure the id is registered in the hardware configuration");
    }

    public PinNumber GetPin(int id)
    {
      if (_pins.ContainsKey(id))
        return _pins[id];
      else
        throw new ArgumentException($"The Pin id '{id}' is unknown. Make sure the id is registered in the hardware configuration");
      
      // var led = LEDs.SingleOrDefault(x => x.Id == id);
      //
      // if(led == null)
      //   throw new ArgumentException($"The Pin id '{id}' is unknown. Make sure the id is registered in the hardware configuration");
      //
      // return led.Pin;
    }
  }
}