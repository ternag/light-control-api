using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Gpio;
using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure 
{
  public class HardwareConfiguration 
  {
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

    public IEnumerable<LED> LEDs { get; }
    
    public int GetPin(int id)
    {
      var led = LEDs.SingleOrDefault(x => x.Id == id);
      if(led == null)
        return -1;

      return led.Pin;
    }
  }
}