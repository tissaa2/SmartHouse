using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.Packets.Processors.CAN;

namespace SmartHouse.Models.Packets.Processors
{
    [PacketType(0x31)]
    public class CANPacketProcessor : PacketProcessor
    {
        public override object ProcessPacket(Packet packet, params object[] args)
        {
            var cp = CANPacket.Read(packet.Data);
            if (CANDataProcessor.Processors.ContainsKey(cp.Command))
            {
                var r = CANDataProcessor.Processors[cp.Command].ProcessData(packet.Data, cp);
                return r;
            }
            return cp;
        }
    }
}
