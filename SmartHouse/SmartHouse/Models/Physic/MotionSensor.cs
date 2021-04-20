using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    [DeviceType(0x38)]
    public class MotionSensor: PDevice
    {
        public override string Icon { get => "pdevice_motionsensor.png"; }
        public override string TypeName { get => "Детектор движения"; }

        public MotionSensor()
        {

        }

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new MotionSensor()).Assign(this);
        }

    }
}
