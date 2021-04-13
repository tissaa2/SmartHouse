using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class MotionSensorModel: DeviceStateModel<bool>
    {
        public override ViewModel Clone()
        {
            var m = new MotionSensorModel();
            m.Assign(this);
            return m;
        }
    }
}
