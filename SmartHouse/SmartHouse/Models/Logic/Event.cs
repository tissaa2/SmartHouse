using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Logic
{
    public class Event: BaseEntity<int>
    {
        public byte InputID { get; set; }
    }
}
