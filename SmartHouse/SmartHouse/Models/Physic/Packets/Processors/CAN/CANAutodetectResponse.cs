using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Physics;
using SmartHouse.Services;

namespace SmartHouse.Models.Packets.Processors.CAN
{
    [CANCommand(0x04)]
    public class CANAutodetectResponseProcessor : CANDataProcessor
    {

        public class ResponseData : CANPacket
        {

            public byte DeviceType;

            public byte OutputsCount;

            public byte InputsCount;

            public byte ScenesCount;

            public short OutputsGroupMask;

            public ResponseData()
            {

            }

            public ResponseData(CANPacket source) : base(source)
            {

            }

            /* public static AutodetectResponse Read(DuplexStream stream)
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
            } */

            public static ResponseData CreateFrom(CANPacket source, PacketDataStream stream)
            {
                ResponseData r = null;
                try
                {
                    r = new ResponseData(source);

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

        public static void AddDevice(ResponseData rd)
        {
            if (PDevice.DeviceTypes.ContainsKey(rd.DeviceType))
            {
                var uid = new UID(rd.UID[2], rd.UID[1], rd.UID[0]);
                var d = PDevice.Get(uid);
                if (d == null)
                {
                    d = Activator.CreateInstance(PDevice.DeviceTypes[rd.DeviceType]) as PDevice;
                    d.Init(BaseEntity.IntID.NewID(), uid, rd.InputsCount, rd.OutputsCount, rd.ScenesCount);
                    PDevice.Add(d);
                    Packet.GetOutputStatesRequest[7] = d.UID.B2;
                    Packet.GetOutputStatesRequest[8] = d.UID.B1;
                    Packet.GetOutputStatesRequest[9] = d.UID.B0;
                    if (Client.CurrentServer != null)
                        Client.CurrentServer.Send(Packet.GetOutputStatesRequest);
                    Log.Write("Device {0} found ", d);
                }
            }
            // Log.Write("Command {0} executed: result = {1}", message, (commandResponsePacket.Result == 0) ? "success" : "failure");
        }

        public override object ProcessData(PacketDataStream stream, CANPacket source)
        {
            var rd = ResponseData.CreateFrom(source, stream);
            // if (rd.StartByte == 0xFF)
            {
                AddDevice(rd);
            }
            base.ProcessData(stream, source);
            return rd;
        }

    }
}
