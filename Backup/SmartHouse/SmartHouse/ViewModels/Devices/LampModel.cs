﻿using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class LampModel: DeviceStateModel<double>
    {
        public override ViewModel Clone()
        {
            var m = new LampModel();
            m.Assign(this);
            return m;
        }
    }
}
