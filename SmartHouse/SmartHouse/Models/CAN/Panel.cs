using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.CAN
{
    public class Panel: Device
    {
        /* public override void Init(int inputsCount, int outputsCount)
        {
            base.Init(inputsCount, outputsCount);
        } */

        public override Device Assign(Device source)
        {
            return base.Assign(source);
        }

        public override Device Clone()
        {
            return (new Panel()).Assign(this);
        }
    }
}
