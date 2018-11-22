using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    [DeviceType(0x08)]
    public class IRTranciever: PDevice
    {
        public override string Icon { get => "pdevice_irtranciever.png"; }
        public override string TypeName { get => "ИК-трансивер"; }
        /* public override void Init(int inputsCount, int outputsCount)
        {
            base.Init(inputsCount, outputsCount);
        } */

        public IRTranciever()
        {

        }

        /* public IRTranciever(UID id, int outputs)
        {
            Init(id, 0, outputs, true);
        } */

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new IRTranciever()).Assign(this);
        }

    }
}
