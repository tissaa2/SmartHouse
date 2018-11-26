using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    [DeviceType(2)]
    public class Relay: PDevice
    {
        public override string Icon { get => "pdevice_relay.png"; }
        public override string TypeName { get => "Реле"; }

        /* public override void Init(int inputsCount, int outputsCount)
        {
            base.Init(inputsCount, outputsCount);
        } */

        public Relay()
        {

        }

        /* public Relay(UID id, int inputs, int outputs)
        {
            Init(id, inputs, outputs, true);
        } */

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new Relay()).Assign(this);
        }

    }
}
