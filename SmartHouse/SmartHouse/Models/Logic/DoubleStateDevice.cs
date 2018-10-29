using Newtonsoft.Json;
using System;

namespace SmartHouse.Models.Logic
{
    public class DoubleStateDevice : StateDevice<double> 
    {
        public DoubleStateDevice()
        {

        }

        public DoubleStateDevice(string name, double state): base (name, state)
        {

        }

        public override void ApplyState(string state)
        {
            double v;
            if (double.TryParse(state, out v))
                State = v;
        }

        public override void SetState(DeviceState state)
        {
            state.Value = State.ToString();
        }

    }
}
