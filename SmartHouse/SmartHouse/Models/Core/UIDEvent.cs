using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.CAN;

namespace SmartHouse.Models.Core
{
    public class UIDEvent: Event
    {
        public UID UID { get; set; }
        public byte TypeID { get; set; }
    }
}
