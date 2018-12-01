using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.Packets.Processors.CAN;

namespace SmartHouse.Models.Packets
{
    public class FileChunkData
    {

        public int Offset;

        public byte[] Data;

        public byte CRC;

        public FileChunkData()
        {

        }

        public static FileChunkData Read(PacketDataStream stream)
        {
            FileChunkData r = null;
            try
            {
                r = new FileChunkData();

                r.Offset = stream.ReadInt32();
                r.Data = stream.Read(stream.Data.Length - 1 - stream.ReadPosition);
                r.CRC = stream.ReadByte();
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
}
