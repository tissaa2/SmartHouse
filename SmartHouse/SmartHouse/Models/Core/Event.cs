using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Core
{
    public class Event: BaseEntity
    {
        public byte InputID { get; set; }
    }
}
