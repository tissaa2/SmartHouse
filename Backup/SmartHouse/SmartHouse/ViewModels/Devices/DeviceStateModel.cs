using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartHouse.ViewModels
{

    public class DeviceStateModel<StateType>: DeviceModel 
    {
        public StateType State;

        public override void Assign(ViewModel source)
        {
            base.Assign(source);
            if (source is DeviceStateModel<StateType>)
            {
                var sm = source as DeviceStateModel<StateType>;
            }
        }
    }
}
