using Newtonsoft.Json;
using SmartHouse.Models.Physics;
using System;

namespace SmartHouse.Models.Logic
{
    public class DoubleStateDevice : Device
    {

        [JsonProperty(PropertyName = "State")]
        public double State { get; set; }

        public DoubleStateDevice()
        {

        }

        public DoubleStateDevice(int id, string name, double state, UID uid, byte portID) : base(id, uid, portID, name, null)
        {
            State = state;
        }

        public override void ApplyState(string state)
        {
            double v;
            if (double.TryParse(state, out v))
            {
                State = v;
            }
        }

        public override DeviceState GetState()
        {
            return new DeviceState() { Value = State.ToString(), DeviceID = ID };
        }

    }
}
