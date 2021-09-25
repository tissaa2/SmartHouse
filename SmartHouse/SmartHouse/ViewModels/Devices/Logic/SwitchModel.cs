﻿using System.Collections.Generic;
using SmartHouse.Models.Storage;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class SwitchModel: DeviceModel
    {
        public override ViewModel Clone()
        {
            var m = new SwitchModel();
            m.Assign(this);
            return m;
        }
    }
}