using SmartHouse.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using SmartHouse.Models.Packets;
using System.Text;

namespace SmartHouse.Models.Packets.Processors
{
    public class PacketProcessor
    {
        private static Dictionary<int, PacketProcessor> processors = null;
        public static Dictionary<int, PacketProcessor> Processors
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
            processors = new Dictionary<int, PacketProcessor>();
            List<Type> list = Utils.FindAllDerivedTypes<PacketProcessor>();

            foreach (Type t in list)
            {
                PacketTypeAttribute packetTypeAttribute = t.GetCustomAttributes(typeof(PacketTypeAttribute), true).FirstOrDefault() as PacketTypeAttribute;
                if (packetTypeAttribute != null)
                {
                    if (!processors.ContainsKey(packetTypeAttribute.Type))
                    {
                        processors.Add(packetTypeAttribute.Type, Activator.CreateInstance(t) as PacketProcessor);
                    }
                    else
                    {
                        Log.Write("Packet processors: Error adding type {0} to packetTypes: type key already exists. Check PaketTypeAttribte value", t);
                    }
                }
            }
        }

        public virtual object ProcessPacket(Packet packet, params object[] args)
        {
            return null;
        }
    }
}
