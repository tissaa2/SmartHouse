using SmartHouse.ViewModels;
using SmartHouse.ViewModels.Devices.Physic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.ViewModels.Devices.Physic
{
    [DeviceType(0x38)]
    public class MotionSensorModel: PhysicDeviceModel
    {
        public override string Icon { get => "pdevice_motionsensor.png"; }
        public override string TypeName { get => "Детектор движения"; }

        public override ViewModel Clone()
        {
            var m = new MotionSensorModel();
            m.Assign(this);
            return m;
        }

    }
}
