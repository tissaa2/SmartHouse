using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{
    
    public class DoubleStateDeviceModel: StateDeviceModel<double> 
    {
        public override ViewModel Clone()
        {
            var m = new DoubleStateDeviceModel();
            m.Assign(this);
            return m;
        }
    }
}
