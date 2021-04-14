using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class DeviceStateModel: ViewModel
    {
        public SceneModel Scene { get; set; }
        public Device Device{ get; set; }
        public bool Enabled { get; set; }

        private string state;
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                if (Enabled)
                    CheckIsDirty(state, value, "State", () => { state = value; });
            }
        }

        public void Apply()
        {
            Device.ApplyState(State);
        }
    }
}
