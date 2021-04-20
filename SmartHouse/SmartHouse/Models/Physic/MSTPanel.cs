using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    // Panel with Motion sensor and termostat
    [DeviceType(0x70)]
    public class MSTPanel: PDevice
    {
        public override string Icon { get => "pdevice_mstpanel.png"; }
        public override string TypeName { get => "Панель с датчиками"; }

        public MSTPanel()
        {

        }

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new MSTPanel()).Assign(this);
        }
    }
}
