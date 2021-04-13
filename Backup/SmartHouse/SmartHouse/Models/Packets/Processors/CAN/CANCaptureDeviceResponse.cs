using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Physics;
using SmartHouse.Services;
using System.Linq;

namespace SmartHouse.Models.Packets.Processors.CAN
{
    [CANCommand(0xAA)]
    public class CANCaptureDeviceResponse : CANDataProcessor
    {
        public class ResponseData : CANPacket
        {
            public byte Response;

            public byte DeviceType;

            public byte InputNumber;

            public byte InputType;



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
                    r.Response = stream.ReadByte();
                    r.DeviceType = stream.ReadByte();
                    r.InputNumber = stream.ReadByte();
                    r.InputType = stream.ReadByte();
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
                return r;
            }

            public override string ToString()
            {
                return string.Format("{0}, UID=({1}), StartByte={2:X}, Command={3}, Response={4}, DeviceType={5:X}, InputNumber={6}, InputType={7:X}",
                    GetType(),
                    BitConverter.ToString(UID).Replace("-", ","),
                    StartByte,
                    Command,
                    Response,
                    DeviceType,
                    InputNumber,
                    InputType
                );
            }
        }

        public delegate void DeviceCapturedDelegate(ResponseData rd);

        public static event DeviceCapturedDelegate OnDeviceCaptured;

        public override object ProcessData(PacketDataStream stream, CANPacket source)
        {
            var rd = ResponseData.CreateFrom(source, stream);
            base.ProcessData(stream, source);
            OnDeviceCaptured?.Invoke(rd);
            return rd;
        }

    }
}
