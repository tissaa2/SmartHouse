using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class SocketModel: DeviceStateModel<double>
    {
        public override ViewModel Clone()
        {
            var m = new SocketModel();
            m.Assign(this);
            return m;
        }
    }
}
