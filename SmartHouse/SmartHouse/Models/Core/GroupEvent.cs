using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Core
{
    public class GroupEvent: Event
    {
        public byte GroupID { get; set; }
        public byte CategoryID { get; set; }
        public byte TimePar { get; set; }
    }
}
