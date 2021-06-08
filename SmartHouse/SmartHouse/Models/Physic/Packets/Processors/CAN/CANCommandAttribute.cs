using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Packets.Processors
{
    public class CANCommandAttribute : Attribute
    {
        public int Command;

        public CANCommandAttribute(int command)
        {
            this.Command = command;
        }
    }
}
