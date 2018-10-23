using System.Collections.Generic;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartHouse.ViewModels
{

    public class StateDeviceModel<StateType>: DeviceModel 
    {
        public static readonly BindableProperty StateProperty = BindableProperty.Create("State", typeof(double), typeof(StateDeviceModel<StateType>), default(double));
        private StateType state;
        [JsonProperty(PropertyName = "State")]
        public StateType State
        {
            get { return state; }
            set { this.state = value; OnPropertyChanged("State"); SetValue(StateProperty, value);  }
        }

        public override void Assign(ViewModel source)
        {
            base.Assign(source);
            if (source is StateDeviceModel<StateType>)
            {
                var sm = source as StateDeviceModel<StateType>;
                this.state = sm.state;
            }
        }
    }
}
