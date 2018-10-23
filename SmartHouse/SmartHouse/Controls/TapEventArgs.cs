using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Controls
{
    public class TapEventArgs: EventArgs
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
