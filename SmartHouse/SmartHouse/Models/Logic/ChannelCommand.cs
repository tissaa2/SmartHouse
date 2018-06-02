using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.Physics;
namespace SmartHouse.Models.Logic
{
    public class ChannelCommand: BaseEntity<UID>
    {
        // public UID UID { get; set; }
        public byte OutputID { get; set; }
    }   
}
