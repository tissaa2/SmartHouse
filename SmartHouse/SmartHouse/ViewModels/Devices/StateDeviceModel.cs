using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartHouse.ViewModels
{

    public class StateDeviceModel<StateType>: DeviceModel 
    {

        public override void Assign(ViewModel source)
        {
            base.Assign(source);
            if (source is StateDeviceModel<StateType>)
            {
                var sm = source as StateDeviceModel<StateType>;
                // this.state = sm.state;
            }
        }
    }
}
