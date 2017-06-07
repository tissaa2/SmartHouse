using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Services.Packets
{
    public class CommandResponsePacket : Packet
    {
        public byte Result = 0;

        public override int Process()
        {
            return 0;
        }

        public override int ReadData(DuplexStream stream)
        {
            base.ReadData(stream);
            int result;
            try
            {
                this.Result = stream.ReadByte();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                result = -1;
                return result;
            }
            result = 0;
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}, Result=({1})", base.ToString(), this.Result);
        }
    }
}
