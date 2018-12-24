using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class SwitchModel: StateDeviceModel<double>
    {
        public override ViewModel Clone()
        {
            var m = new SwitchModel();
            m.Assign(this);
            return m;
        }
    }
}
