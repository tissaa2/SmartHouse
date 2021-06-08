using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.Packets.Processors.CAN;

namespace SmartHouse.Models.Packets
{
    public class FileHeaderData
    {

        public int Size;

        public FileHeaderData()
        {

        }

        public static FileHeaderData Read(PacketDataStream stream)
        {
            FileHeaderData r = null;
            try
            {
                r = new FileHeaderData();
                r.Size = stream.ReadInt32();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return r;
        }

        public override string ToString()
        {
            return string.Format("{0}, Size={1}",
                GetType(),
                this.Size
            );
        }
    }
}
