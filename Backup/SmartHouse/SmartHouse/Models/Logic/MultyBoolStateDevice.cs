using Newtonsoft.Json;
using SmartHouse.Models.Physics;
using System;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{
    public class MultyBoolStateDevice : /* StateDevice<double> */ Device 
    {

        [JsonProperty(PropertyName = "State")]
        public List<bool> State { get; set; }
        //private List<bool> state;
        //[JsonProperty(PropertyName = "State")]
        //public List<bool> State
        //{
        //    get { return state; }
        //    set
        //    {
        //        state = value;
        //        // Port.SetPortValue(ID, PortID, (byte)state, 2);
        //        OnPropertyChanged("State");
        //    }
        //}

        public MultyBoolStateDevice()
        {

        }

        public MultyBoolStateDevice(int id, string name, IEnumerable<bool> state, UID uid, byte portID): base (id, uid, portID, name, null)
        {
            State = new List<bool>(state);
        }

        public override void ApplyState(string state)
        {
        }

        public override void SetState(DeviceState state)
        {
        }

    }
}
