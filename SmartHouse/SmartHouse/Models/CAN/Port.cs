using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.CAN
{
    public class Port
    {
        public int ID { get; set; }
        public double Value { get; set; }
        public virtual Port Clone()
        {
            return null;
        }
    }
}
