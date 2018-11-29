using Newtonsoft.Json;
using SmartHouse.Models.Physics;
using System;

namespace SmartHouse.Models.Logic
{
    public class BoolStateDevice : Device /* StateDevice<bool> */ 
    {
        private bool state;
        [JsonProperty(PropertyName = "State")]
        public bool State
        {
            get { return state; }
            set
            {
                state = value;
                Port.SetPortValue(ID, PortID, (byte)(state ? 1 : 0));
                OnPropertyChanged("State");
            }
        }

        public BoolStateDevice()
        {

        }

        public BoolStateDevice(string name, bool state): base (UIDID.NewID(), name, null)
        {
            State = state;
        }

        public override void ApplyState(string state)
        {
            bool v;
            if (bool.TryParse(state, out v))
                State = v;

            // сюда вставить код изменения состояния выхода диммера
        }

        public override void SetState(DeviceState state)
        {
            state.Value = State.ToString();
        }

    }
}
