using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    [DeviceType(0x58)]
    public class IRPanel: PDevice
    {
        public override string Icon { get => "pdevice_irpanel.png"; }
        public override string TypeName { get => "ИК-панель"; }

        public IRPanel()
        {

        }

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new IRPanel()).Assign(this);
        }
    }
}
