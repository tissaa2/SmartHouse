using Newtonsoft.Json;
using SmartHouse.Models.Physics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHouse.Models.Logic
{
    public class MultyBoolStateDevice : /* StateDevice<double> */ Device 
    {

        [JsonProperty(PropertyName = "State")]
        public List<bool> State { get; set; }


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

        public override DeviceState GetState()
        {
            return new DeviceState() { Value = string.Join("", State.Select(e => e ? "1": "0")) , DeviceID = ID };
        }

    }
}
