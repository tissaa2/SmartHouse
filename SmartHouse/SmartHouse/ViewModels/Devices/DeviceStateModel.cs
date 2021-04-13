using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class DeviceStateModel: ViewModel
    {
        public Device Device{ get; set; }

        private string state;
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                CheckIsDirty(state, value, "State", () => { state = value; });
            }
        }

        public void Apply()
        {
            Device.ApplyState(StateValue);
        }
    }
}
