using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Gpio;
using LightControl.Api.Models;

namespace LightControl.Api.Infrastructure
{
  public class LED
  {
    public LED(int id, int pin)
    {
      Id = id;
      Pin = pin;
    }

    public int Id { get; }
    public int Pin { get; }
  }
}