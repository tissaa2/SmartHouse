using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.CAN;
namespace SmartHouse.Models.Core
{
    public class ChannelCommand: BaseEntity
    {
        public UID UID { get; set; }
        public byte OutputID { get; set; }
    }
}
