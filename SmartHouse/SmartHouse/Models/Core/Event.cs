using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Core
{
    public class Event: BaseEntity<int>
    {
        public byte InputID { get; set; }
    }
}
