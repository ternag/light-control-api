using System.Device.Gpio;
using System.Device.I2c;
using LightControl.Api.Models;
using Microsoft.Extensions.Logging;

namespace LightControl.Api.Infrastructure
{
    public class RaspberryPiHAL : IHal
    {
        private HardwareConfiguration _hw;
        private I2cDevice _device1;
        public RaspberryPiHAL()
        {
            _hw = new HardwareConfiguration();

            //I2cConnectionSettings settings = new I2cConnectionSettings(_hw.Mcp23017.Bus, _hw.Mcp23017.Id);
            //_device1 = I2cDevice.Create(settings);
        }

        public void Update(Led led)
        {
            IDevice device = _hw.GetDevice(led.Id);
            var pin = _hw.GetPin(led.Id);
            device.Write(pin, led.State.ToPinValue());
        }
    }
}