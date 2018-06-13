using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Logic;

namespace SmartHouse.Models.Physics
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
