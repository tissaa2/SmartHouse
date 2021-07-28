using System.Collections.Generic;
using SmartHouse.Models.Storage;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class SocketModel: DeviceModel
    {
        public override ViewModel Clone()
        {
            var m = new SocketModel();
            m.Assign(this);
            return m;
        }
    }
}
