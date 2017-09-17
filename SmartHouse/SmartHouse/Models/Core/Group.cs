using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.CAN;
using System.Xml.Serialization;

namespace SmartHouse.Models.Core
{
    public class Group: BaseEntity
    {
        public int Id { get; set; }
        public List<Event> Events { get; set; }
        public List<ChannelCommand> ChannelCommands { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
