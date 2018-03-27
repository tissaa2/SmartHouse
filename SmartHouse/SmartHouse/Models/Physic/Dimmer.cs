using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.CAN
{
    public class Dimmer: Device
    {
        /* public override void Init(int inputsCount, int outputsCount)
        {
            base.Init(inputsCount, outputsCount);
        } */

        public Dimmer()
        {

        }

        public Dimmer(UID id, int outputs)
        {
            Init(id, 0, outputs, true);
        }

        public override Device Assign(Device source)
        {
            return base.Assign(source);
        }

        public override Device Clone()
        {
            return (new Dimmer()).Assign(this);
        }

    }
}
