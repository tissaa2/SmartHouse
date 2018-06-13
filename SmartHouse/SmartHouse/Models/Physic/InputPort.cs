using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    public class InputPort: Port
    {
        public override Port Clone()
        {
            return new InputPort() { ID = this.ID, Value = this.Value};
        }
    }
}
