using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Packets
{
    public class CANResponse: PacketData
    {
        public byte ConfigByte;

        public static int UID_SIZE = 3;

        public byte[] UID = new byte[UID_SIZE];

        public byte StartByte;

        public byte Command;

        public virtual void Assign(CANResponse source)
        {
            ConfigByte = source.ConfigByte;
            UID = source.UID;
            StartByte = source.StartByte;
            Command = source.Command;
        }

        public virtual void ReadFrom(DuplexStream stream)
        {
            try
            {
                ConfigByte = stream.ReadByte();
                UID = stream.ReadBytes(UID_SIZE);
                StartByte = stream.ReadByte();
                Command = stream.ReadByte();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        public virtual void ReadFrom(byte[] buffer, ref int pos)
        {
            try
            {
                ConfigByte = buffer.ReadByte(ref pos);
                UID = buffer.ReadBytes(UID_SIZE, ref pos);
                StartByte = buffer.ReadByte(ref pos);
                Command = buffer.ReadByte(ref pos);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        public static CANResponse Read(DuplexStream stream)
        {
            CANResponse cr = new CANResponse();
            cr.ReadFrom(stream);
            return cr;
        }

        public static CANResponse Read(byte[] buffer, ref int pos)
        {
            CANResponse cr = new CANResponse();
            cr.ReadFrom(buffer, ref pos);
            return cr;
        }

        public CANResponse()
        {

        }

        public CANResponse(CANResponse source)
        {
            Assign(source);
        }


        public override string ToString()
        {
            return string.Format("{0}, UID=({1}), StartByte={2:X}, Command={3}", 
                GetType(),
                BitConverter.ToString(this.UID).Replace("-", ","),
                this.StartByte,
                this.Command
            );
        }
    }
}
