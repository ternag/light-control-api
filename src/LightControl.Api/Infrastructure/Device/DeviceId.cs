using System;

namespace LightControl.Api.Infrastructure.Device
{
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