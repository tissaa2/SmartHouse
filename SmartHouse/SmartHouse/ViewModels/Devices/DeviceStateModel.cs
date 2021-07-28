using System.Collections.Generic;
using SmartHouse.Models.Storage;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class DeviceStateModel: ViewModel
    {
        public SceneModel Scene { get; set; }
        public int DeviceID{ get; set; }
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

    }
}
