using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Packets
{
    public class CommandConfirmation
    {
        public static CommandConfirmation Read(PacketDataStream stream)
        {
            return new CommandConfirmation() { Result = stream.ReadByte() };
        }

        public Byte Result = 0;

        public override string ToString()
        {
            return string.Format("{0}, Result=({1})", base.ToString(), this.Result);
        }
    }
}
