using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Packets
{
    [PacketType(0)]
    public class ControllerDiscoverPacketData
    {
        public static int UID_SIZE = 3;

        public byte[] UID = new byte[UID_SIZE];

        public byte PortMask = 0;

        public int IpAddress = 0;

        public byte PortNumber = 0;

        public static ControllerDiscoverPacketData Read(PacketDataStream stream)
        {
            ControllerDiscoverPacketData r = null;
            try
            {
                r = new ControllerDiscoverPacketData();
                r.UID = stream.Read(UID_SIZE);
                r.PortMask = stream.ReadByte();
                r.IpAddress = stream.ReadInt32();
                r.PortNumber = stream.ReadByte();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return r;
        }

        /* public static ControllerDiscoverResponse Read(byte[] buffer, ref int pos)
        {
            ControllerDiscoverResponse r = null;
            try
            {
                r = new ControllerDiscoverResponse();
                r.UID = buffer.ReadBytes(UID_SIZE, ref pos);
                r.PortMask = buffer.ReadByte(ref pos);
                r.IpAddress = buffer.ReadInt32(ref pos);
                r.PortNumber = buffer.ReadByte(ref pos);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return r;
        } */

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
