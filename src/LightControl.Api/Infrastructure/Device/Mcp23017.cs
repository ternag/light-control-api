using System;
using System.Device.Gpio;

namespace LightControl.Api.Infrastructure.Device
{
  public class Mcp23017 : IDevice
    {
        
        public Mcp23017(DeviceId id)
        {
            Id = id;
            // PortA = 0;
            // PortB = 0;
        }
        public DeviceId Id { get; }
        public int Bus => 1;

        //public byte PortA { get; set; }
        //public byte PortB { get; set; }

        public void Write(int pin, PinValue value)
        {

        }
    }

    public class DeviceId
    {
        public DeviceId(int id)
        {
            if (id < 0x20 || id > 0x27) throw new ArgumentException("id range is [0x20 .. 0x27]");
            Value = id;
        }

        public int Value { get; }

        public static implicit operator int(DeviceId id)
        {
            return id.Value;
        }

        public static explicit operator DeviceId(int value)
        {
            return new DeviceId(value);
        }
    }
}