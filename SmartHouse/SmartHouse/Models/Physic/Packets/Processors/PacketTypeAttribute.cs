using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Packets
{
    public class PacketTypeAttribute : Attribute
    {
        public int Type;

        public PacketTypeAttribute(int type)
        {
            this.Type = type;
        }
    }
}
