using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Core;

namespace SmartHouse.Models.CAN
{
    public class Port: IconEntity<int>
    {
        public double Value { get; set; }
        public virtual Port Clone()
        {
            return null;
        }
    }
}
