using SmartHouse.ViewModels;
using SmartHouse.ViewModels.Devices.Physic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Devices.Physic
{
    // Panel with Motion sensor and termostat
    [DeviceType(0x70)]
    public class MSTPanelModel: PhysicDeviceModel
    {
        public override string Icon { get => "pdevice_mstpanel.png"; }
        public override string TypeName { get => "Панель с датчиками"; }

        public override ViewModel Clone()
        {
            var m = new MSTPanelModel();
            m.Assign(this);
            return m;
        }
    }
}
