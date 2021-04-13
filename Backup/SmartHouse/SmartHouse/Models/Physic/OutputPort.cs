using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    public class OutputPort: Port
    {
        public override string Icon { get => "outputport.png"; }
        // public override string Name { get => "outputport.png"; }
        public override string Name { get => String.Format("Выход {0}", ID); }

        public override Port Clone()
        {
            // this.Name
            return new OutputPort() { ID = this.ID, Value = this.Value };
        }
    }
}
