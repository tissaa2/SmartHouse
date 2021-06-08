using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.Packets.Processors.CAN;

namespace SmartHouse.Models.Packets.Processors
{
    // [PacketType(0x58)]
    public class FileChunkProcessor : PacketProcessor
    {
        public class ResponseData 
        {

            public int Offset;

            public byte[] Data;

            public ResponseData()
            {

            }

            public static ResponseData Read(PacketDataStream stream)
            {
                ResponseData r = null;
                try
                {
                    r = new ResponseData();

                    r.Offset = stream.ReadInt32();
                    r.Data = stream.Read();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
                return r;
            }

            public override string ToString()
            {
                return string.Format("{0}, Offset={1}, Data=({2})",
                    GetType(),
                    this.Offset,
                    BitConverter.ToString(this.Data).Replace("-", ",")
                );
            }
        }

        public override object ProcessPacket(Packet packet, params object[] args)
        { 
            var rd = ResponseData.Read(packet.Data);

            if (Client.CurrentServer.CurrentFile != null)
            {
                Client.CurrentServer.CurrentFile.Write(rd.Offset, rd.Data);
            }

            return rd;
        }
    }
}
