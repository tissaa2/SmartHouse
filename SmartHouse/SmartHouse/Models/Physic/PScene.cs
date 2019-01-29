using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    public class PScene
    {
        public Dictionary<int, double> OutputStates { get; set; } = new Dictionary<int, double>();
        public int ID { get; set; }
        public UID SourceID { get; set; }
        public byte SourcePort { get; set; }
        public byte SourceType { get; set; } = 0x05;
        // public Event

        public PScene(int id, UID sourceID, byte sourcePort, byte sourceType)
        {
            ID = id;
            SourceID = sourceID;
            SourcePort = sourcePort;
            SourceType = SourceType;
        }
    }
}
