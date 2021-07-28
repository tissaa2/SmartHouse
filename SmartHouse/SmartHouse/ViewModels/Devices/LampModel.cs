﻿using System.Collections.Generic;
using SmartHouse.Models.Storage;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class LampModel: DeviceModel
    {
        public override ViewModel Clone()
        {
            var m = new LampModel();
            m.Assign(this);
            return m;
        }
    }
}
