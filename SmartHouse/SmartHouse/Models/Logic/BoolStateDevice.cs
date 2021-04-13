using Newtonsoft.Json;
using SmartHouse.Models.Physics;
using System;

namespace SmartHouse.Models.Logic
{
    public class BoolStateDevice : Device
    {
        public bool State { get; set; }

        public BoolStateDevice()
        {

        }

        public BoolStateDevice(int id, string name, bool state, UID uid, byte portID) : base(id, uid, portID, name, null )
        {
            State = state;
        }

        public override DeviceState GetState()
        {
            return new DeviceState() { Value = State.ToString(), DeviceID = ID };
        }

    }
}
