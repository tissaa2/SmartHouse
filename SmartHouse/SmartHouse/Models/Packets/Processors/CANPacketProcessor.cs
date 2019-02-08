using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartHouse.Services;
using SmartHouse.Models.Packets.Processors.CAN;
using System.Threading.Tasks;

namespace SmartHouse.Models.Packets.Processors
{
    [PacketType(0x31)]
    public class CANPacketProcessor : PacketProcessor
    {
        private static CANPacketProcessor instance = null;
        public static CANPacketProcessor Instance {
            get
            {
                if (instance == null)
                {
                    instance = PacketProcessor.Processors.Values.FirstOrDefault(e => e is CANPacketProcessor) as CANPacketProcessor;
                }
                return instance;
            }
        }
        public CANPacketProcessor()
        {

        }

        public void AddPacketCallback(CANActionCallback callback)
        {
            lock (PacketCallbacks)
            {
                Dictionary<UID, List<CANActionCallback>> d;
                if (PacketCallbacks.ContainsKey(callback.Command))
                    d = PacketCallbacks[callback.Command];
                else
                {
                    d = new Dictionary<UID, List<CANActionCallback>>();
                    PacketCallbacks.Add(callback.Command, d);
                }

                List<CANActionCallback> cbl;
                if (d.ContainsKey(callback.UID))
                    cbl = d[callback.UID];
                else
                {
                    cbl = new List<CANActionCallback>();
                    d.Add(callback.UID, cbl);
                }
                cbl.Add(callback);
                ExpirationQueue.Add(callback);
            }
        }

        public Dictionary<int, Dictionary<UID, List<CANActionCallback>>> PacketCallbacks { get; set; } = new Dictionary<int, Dictionary<UID, List<CANActionCallback>>>();
        public List<CANActionCallback> ExpirationQueue { get; set; } = new List<CANActionCallback>();
        public override object ProcessPacket(Packet packet, params object[] args)
        {
            var cp = CANPacket.Read(packet.Data);
            if (PacketCallbacks.ContainsKey(cp.Command))
            {
                var d = PacketCallbacks[cp.Command];
                var id = new UID(cp.UID[2], cp.UID[1], cp.UID[0]);
                if (d.ContainsKey(id))
                {
                    var cbl = d[id];
                    var rl = new List<CANActionCallback>();
                    foreach (var c in cbl)
                    {
                        bool r = true;

                        if (c.Callback != null)
                            if (c.ExpireTime < DateTime.Now.Ticks)
                                c.Callback(cp, true);
                            else
                                r = c.Callback(cp, false);
                        if (r)
                            rl.Add(c);
                    }

                    foreach (var c in rl)
                        cbl.Remove(c);

                    if (cbl.Count < 1)
                    {
                        d.Remove(id);
                        if (d.Count < 1)
                            PacketCallbacks.Remove(cp.Command);
                    }

                }
            }

            if (CANDataProcessor.Processors.ContainsKey(cp.Command))
            {
                var r = CANDataProcessor.Processors[cp.Command].ProcessData(packet.Data, cp);
                return r;
            }
            return cp;
        }

        public bool RemoveExpired()
        {
            bool r = false;
            lock(PacketCallbacks)
            {
                var rl = new List<CANActionCallback>();
                foreach(var e in ExpirationQueue)
                {
                    if (e.ExpireTime < DateTime.Now.Ticks)
                    {
                        e.Callback(null, true);
                        rl.Add(e);
                        var d = PacketCallbacks[e.Command];
                        d.Remove(e.UID);
                        if (d.Count < 1)
                            PacketCallbacks.Remove(e.Command);
                        r = true;
                    }
                }
                foreach (var e in rl)
                    ExpirationQueue.Remove(e);
            }
            return r;
        }
    }
}
