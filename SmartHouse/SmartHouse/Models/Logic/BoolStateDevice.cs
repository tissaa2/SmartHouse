using Newtonsoft.Json;
using System;

namespace SmartHouse.Models.Logic
{
    public class BoolStateDevice : StateDevice<bool> 
    {
        public BoolStateDevice()
        {

        }

        public BoolStateDevice(string name, bool state): base (name, state)
        {

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
