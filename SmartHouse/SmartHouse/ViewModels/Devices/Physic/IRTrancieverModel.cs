using SmartHouse.ViewModels;
using SmartHouse.ViewModels.Devices.Physic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Devices.Physic
{
    [DeviceType(0x08)]
    public class IRTranscieverModel: PhysicDeviceModel
    {
        public override string Icon { get => "pdevice_irtranciever.png"; }
        public override string TypeName { get => "ИК-трансивер"; }

        public override ViewModel Clone()
        {
            var m = new IRTranscieverModel();
            m.Assign(this);
            return m;
        }

    }
}
