using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    [DeviceType(0x08)]
    public class IRTransciever: PDevice
    {
        public override string Icon { get => "pdevice_irtranciever.png"; }
        public override string TypeName { get => "ИК-трансивер"; }

        public IRTransciever()
        {

        }

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new IRTransciever()).Assign(this);
        }

    }
}
