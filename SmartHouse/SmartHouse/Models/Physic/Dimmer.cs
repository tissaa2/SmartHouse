using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    [DeviceType(1)]
    public class Dimmer: PDevice
    {
        public override string Icon { get => "pdevice_dimmer.png"; }
        public override string TypeName { get => "Диммер"; }

        /* public override void Init(int inputsCount, int outputsCount)
        {
            base.Init(inputsCount, outputsCount);
        } */

        public Dimmer()
        {

        }

        /* public Dimmer(UID id, int inputs, int outputs)
        {
            Init(id, inputs, outputs, true);
        } */

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new Dimmer()).Assign(this);
        }

    }
}
