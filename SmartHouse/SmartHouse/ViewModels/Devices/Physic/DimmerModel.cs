using SmartHouse.Models.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Devices.Physic
{
    [DeviceType(1)]
    public class DimmerModel: PhysicDeviceModel
    {
        public override string Icon { get => "pdevice_dimmer.png"; }
        public override string TypeName { get => "Диммер"; }

        public override ViewModel Clone()
        {
            var m = new DimmerModel();
            m.Assign(this);
            return m;
        }

    }
}
