using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Packets
{
    [PacketType(0)]
    public class DiscoverResponsePacket : Packet
    {
        public static byte UID_SIZE = 3;

        public byte[] UID = new byte[(int)DiscoverResponsePacket.UID_SIZE];

        public byte PortMask = 0;

        public int IpAddress = 0;

        public byte PortNumber = 0;

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
                this.UID = stream.ReadBytes((int)DiscoverResponsePacket.UID_SIZE);
                this.PortMask = stream.ReadByte();
                this.IpAddress = stream.ReadInt32();
                this.PortNumber = stream.ReadByte();
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
            return string.Format("{0}, UID=({1}), PortMask={2} IpAddress={3}, PortNumber={4}", new object[]
            {
                base.ToString(),
                BitConverter.ToString(this.UID).Replace("-", ","),
                this.PortMask,
                this.IpAddress,
                this.PortNumber
            });
        }
    }
}
