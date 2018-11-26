using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Packets
{
    public class CommandConfirmation: PacketData
    {
        public static CommandConfirmation Read(byte[] buffer, ref int pos)
        {
            return new CommandConfirmation() { Result = buffer.ReadByte(ref pos) };
        }

        public Byte Result = 0;

        public override string ToString()
        {
            return string.Format("{0}, Result=({1})", base.ToString(), this.Result);
        }
    }
}
