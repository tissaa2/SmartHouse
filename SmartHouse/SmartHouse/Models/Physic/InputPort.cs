using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    public class InputPort: Port
    {
        public override string Icon { get => "inputport.png"; }
        public override string Name { get => String.Format("Вход {0}", ID); }

        public override Port Clone()
        {
            return new InputPort() { ID = this.ID, Value = this.Value};
        }
    }
}
