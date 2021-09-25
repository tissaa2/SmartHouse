using SmartHouse.ViewModels;
using SmartHouse.ViewModels.Devices.Physic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Devices.Physic
{
    [DeviceType(0x58)]
    public class IRPanelModel: PhysicDeviceModel
    {
        public override string Icon { get => "pdevice_irpanel.png"; }
        public override string TypeName { get => "ИК-панель"; }

        public override ViewModel Clone()
        {
            var panel = new IRPanelModel();
            panel.Assign(this);
            return panel;
        }
    }
}
