using Newtonsoft.Json;
using SmartHouse.Models.Physics;
using System;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{
    public class MultyBoolStateDevice : /* StateDevice<double> */ Device 
    {

        private List<bool> state;
        [JsonProperty(PropertyName = "State")]
        public List<bool> State
        {
            get { return state; }
            set
            {
                state = value;
                // Port.SetPortValue(ID, PortID, (byte)state, 2);
                OnPropertyChanged("State");
            }
        }

        public MultyBoolStateDevice()
        {

        }

        public MultyBoolStateDevice(string name, IEnumerable<bool> _state): base (UIDID.NewID(), name, null)
        {
            state = new List<bool>(_state);
        }

        public override void ApplyState(string state)
        {
        }

        public override void SetState(DeviceState state)
        {
        }

    }
}
