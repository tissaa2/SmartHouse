using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class PanelModel: DeviceModel
    {
        public override ViewModel Clone()
        {
            var m = new PanelModel();
            m.Assign(this);
            return m;
        }
    }
}
