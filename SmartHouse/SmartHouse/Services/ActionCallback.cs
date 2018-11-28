using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Services
{
    public delegate void ActionCallbackDelegate(object data);
    public class ActionCallback
    {
        public long ExpireTime { get; set; } = -1;
        public int Data { get; set; }
        public ActionCallbackDelegate Callback { get; set; } 
    }
}
