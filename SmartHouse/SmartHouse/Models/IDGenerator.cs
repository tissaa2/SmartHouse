using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models
{
    public class IDGenerator<IDType>
    {
        public delegate object IncDelegate(object value);
        IncDelegate Inc { get; set; }

        public IDType nextID;
        public IDType NewID()
        {
            nextID = (IDType)Inc(nextID);
            return nextID;
        }

        public IDGenerator(IncDelegate inc)
        {
            Inc = inc;
        }
    }
}
