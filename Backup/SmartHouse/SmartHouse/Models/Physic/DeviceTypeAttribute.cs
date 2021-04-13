using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    public class DeviceTypeAttribute : Attribute
    {
        public byte Type;

        public DeviceTypeAttribute(byte type)
        {
            this.Type = type;
        }
    }
}
