using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Packets
{
    public class AutodetectResponse: PacketData
    {
        public static int UID_SIZE = 4;

        public byte[] UID = new byte[UID_SIZE];

        public byte StartByte;

        public byte Command;

        public byte DeviceType;

        public byte OutputsCount;

        public byte InputsCount;

        public byte ScenesCount;

        public short OutputsGroupMask;

        public static AutodetectResponse Read(DuplexStream stream)
        {
            AutodetectResponse r = null;
            try
            {
                r = new AutodetectResponse();
                r.UID = stream.ReadBytes(UID_SIZE);
                r.StartByte = stream.ReadByte();
                r.Command = stream.ReadByte();
                r.DeviceType = stream.ReadByte();

                r.OutputsCount = stream.ReadByte();
                r.InputsCount = stream.ReadByte();
                r.ScenesCount = stream.ReadByte();
                r.OutputsGroupMask = stream.ReadInt16();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return r;
        }

        public override string ToString()
        {
            return string.Format("{0}, UID=({1}), StartByte={2:X}, Command={3}, DeviceType={4:X}, OutputsCount={5}, InputsCount={6}, ScenesCount={7}, OutputsGroupMask={8:X}", 
                GetType(),
                BitConverter.ToString(this.UID).Replace("-", ","),
                this.StartByte,
                this.Command,
                this.DeviceType,
                this.OutputsCount,
                this.InputsCount,
                this.ScenesCount,
                this.OutputsGroupMask
            );
        }
    }
}
