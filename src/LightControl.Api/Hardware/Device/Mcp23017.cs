using System.Device.Gpio;
using LightControl.Api.Infrastructure;

namespace LightControl.Api.Hardware.Device
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

        public void Write(PinNumber pin, PinValue value)
        {

        }

        public string Name => "MCP23017";
    }
}