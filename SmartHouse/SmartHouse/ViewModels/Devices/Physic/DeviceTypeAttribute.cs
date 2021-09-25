using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Devices.Physic
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
