using System.Collections.Generic;
using SmartHouse.Models.Storage;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{
    
    public class FanModel: DeviceModel 
    {
        public override ViewModel Clone()
        {
            var m = new FanModel();
            m.Assign(this);
            return m;
        }
    }
}
