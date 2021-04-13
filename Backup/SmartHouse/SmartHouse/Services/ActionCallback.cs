using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Services
{
    // public delegate bool ActionCallbackDelegate(object data, bool isExpired);
    public class ActionCallback
    {
        public long ExpireTime { get; set; } = -1;
        public int Command { get; set; }
        public ProcessPacketDelegate Callback { get; set; } 
    }
}
