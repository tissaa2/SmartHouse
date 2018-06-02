using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    public class OutputPort: Port
    {
        public override Port Clone()
        {
            return new OutputPort() { ID = this.ID, Value = this.Value };
        }
    }
}
