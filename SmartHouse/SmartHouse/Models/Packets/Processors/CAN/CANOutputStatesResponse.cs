using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Physics;
using SmartHouse.Services;
using System.Linq;

namespace SmartHouse.Models.Packets.Processors.CAN
{
    [CANCommand(0x5A)]
    public class CANOutputStatesResponse : CANDataProcessor
    {

        public class ResponseData : CANPacket
        {
            public byte DeviceType;

            public byte SubCode;

            public byte OutputsActivity;

            public byte StartOutputNumber;

            public byte[] OutputValues;

            public byte LightsMask;

            public byte OutputsTemporalActivity;

            public ResponseData()
            {

            }

            public ResponseData(CANPacket source) : base(source)
            {

            }

            public static ResponseData CreateFrom(CANPacket source, PacketDataStream stream)
            {
                ResponseData r = null;
                try
                {
                    r = new ResponseData(source);

                    byte v = stream.ReadByte();
                    r.DeviceType = (byte)(v >> 4);
                    r.SubCode = (byte)(v & 0x0F);

                    v = stream.ReadByte();
                    r.OutputsActivity = (byte)(v >> 4);
                    r.StartOutputNumber = (byte)(v & 0x0F);

                    r.OutputValues = stream.Read(4);

                    v = stream.ReadByte();
                    r.LightsMask = (byte)(v >> 4);
                    r.OutputsTemporalActivity = (byte)(v & 0x0F);

                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
                return r;
            }

            /* public static AutodetectResponse CreateFrom(CANResponse source, DuplexStream stream)
            {
                AutodetectResponse r = null;
                try
                {
                    r = new AutodetectResponse(source);

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

            public static AutodetectResponse CreateFrom(CANResponse source, byte[] buffer, ref int pos)
            {
                AutodetectResponse r = null;
                try
                {
                    r = new AutodetectResponse(source);

                    r.DeviceType = buffer.ReadByte(ref pos);
                    r.OutputsCount = buffer.ReadByte(ref pos);
                    r.InputsCount = buffer.ReadByte(ref pos);
                    r.ScenesCount = buffer.ReadByte(ref pos);
                    r.OutputsGroupMask = buffer.ReadInt16(ref pos);
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
                return r;
            } */

            public override string ToString()
            {
                return string.Format("{0}, UID=({1}), StartByte={2:X}, Command={3}, DeviceType={4:X}, SubCode={5}, OutputsActivity={6}, StartOutputNumber={7}, OutputValues={8}, LightsMask={9}, OutputsTemporalActivity={10}",
                    GetType(),
                    BitConverter.ToString(UID).Replace("-", ","),
                    StartByte,
                    Command,
                    DeviceType,
                    SubCode,
                    OutputsActivity,
                    StartOutputNumber,
                    BitConverter.ToString(OutputValues).Replace("-", ","),
                    LightsMask,
                    OutputsTemporalActivity
                );
            }
        }

        public static void UpdateDevice(ResponseData rd)
        {
            var id = new UID(rd.UID[2], rd.UID[1], rd.UID[0]);
            var d = PDevice.Get(id);
            if (d != null)
            {
                int i = rd.StartOutputNumber;
                d.Outputs[i].SetLocalValue(rd.OutputValues[0]);
                d.Outputs[i+1].SetLocalValue(rd.OutputValues[1]);
                d.Outputs[i+2].SetLocalValue(rd.OutputValues[2]);
                d.Outputs[i+3].SetLocalValue(rd.OutputValues[3]);
            }
            // Log.Write("Command {0} executed: result = {1}", message, (commandResponsePacket.Result == 0) ? "success" : "failure");
        }

        public override object ProcessData(PacketDataStream stream, CANPacket source)
        {
            var rd = ResponseData.CreateFrom(source, stream);
            // if (rd.StartByte == 0xFF)
            {
                UpdateDevice(rd);
            }
            base.ProcessData(stream, source);
            return rd;
        }

    }
}
