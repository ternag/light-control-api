using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Gpio;
using LightControl.Api.Models;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using LightControl.Api.Hardware;

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
    public HardwareConfiguration()
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
      
    }

    private ReadOnlyDictionary<int, IDevice> Devices {get;}
    private ReadOnlyDictionary<int, PinNumber> Pins {get;}

    public IEnumerable<LED> LEDs { get; }
    
    public IDevice GetDevice(PinNumber id)
    {
      throw new NotImplementedException();
    }

    public PinNumber GetPin(int id)
    {
      var led = LEDs.SingleOrDefault(x => x.Id == id);

      if(led == null)
        throw new ArgumentException($"The Pin id '{id}' is unknown. Make sure the id is registered in the hardware sonfiguration");

      return led.Pin;
    }
  }
}