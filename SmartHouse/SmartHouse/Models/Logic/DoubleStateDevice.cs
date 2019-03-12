using Newtonsoft.Json;
using SmartHouse.Models.Physics;
using System;

namespace SmartHouse.Models.Logic
{
    public class DoubleStateDevice : /* StateDevice<double> */ Device
    {

        private double state;
        [JsonProperty(PropertyName = "State")]
        public double State
        {
            get { return state; }
            set
            {
                state = value > 90 ? 100 : value < 10 ? 0 : value;
                if (Initialized)
                    Port.SetPortValue(UID, PortID, (byte)state, 2);
                OnPropertyChanged("State");
            }
        }

        public DoubleStateDevice()
        {

        }

        // public DoubleStateDevice(string name, double state) : base(UIDID.NewID(), name, null)
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
                // Port.SetPortValue(UID, PortID, (byte)v, 2);
            }
        }

        public override void SetState(DeviceState state)
        {
            state.Value = State.ToString();
        }

    }
}
