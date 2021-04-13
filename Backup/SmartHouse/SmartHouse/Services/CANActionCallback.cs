using SmartHouse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Services
{
    public class CANActionCallback
    {
        public long ExpireTime { get; set; } = -1;
        public int Command { get; set; }
        public UID UID { get; set; }
        public ProcessCANPacketDelegate Callback { get; set; }
    }
}
