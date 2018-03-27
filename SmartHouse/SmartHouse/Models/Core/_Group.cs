using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.CAN;
using System.Xml.Serialization;

namespace SmartHouse.Models.Core
{
    public class _Group: BaseEntity<int>
    {
        public List<Event> Events { get; set; }
        public List<ChannelCommand> ChannelCommands { get; set; }
    }
}
