using SmartHouse.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHouse.Models.Packets.Processors.CAN
{
    public class CANDataProcessor
    {
        private static Dictionary<int, CANDataProcessor> processors = null;
        public static Dictionary<int, CANDataProcessor> Processors
        {
            get
            {
                if (processors == null)
                {
                    LoadProcessors();
                }
                return processors;
            }
        }

        public static void LoadProcessors()
        {
            processors = new Dictionary<int, CANDataProcessor>();
            List<Type> list = Utils.FindAllDerivedTypes<CANDataProcessor>();

            foreach (Type t in list)
            {
                var attr = t.GetCustomAttributes(typeof(CANCommandAttribute), true).FirstOrDefault() as CANCommandAttribute;
                if (attr != null)
                {
                    if (!processors.ContainsKey(attr.Command))
                    {
                        processors.Add(attr.Command, Activator.CreateInstance(t) as CANDataProcessor);
                    }
                    else
                    {
                        Log.Write("Packet processors: Error adding type {0} to packetTypes: type key already exists. Check PaketTypeAttribte value", t);
                    }
                }
            }
        }

        public virtual object ProcessData(PacketDataStream stream, CANPacket source)
        {
            return null;
        }
    }
}
