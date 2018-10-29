using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace SmartHouse.Models.Logic
{
    public class StateDevice<StateType> : Device 
    {

        /* public static readonly BindableProperty StateProperty = BindableProperty.Create("State", typeof(StateType), typeof(StateDevice<StateType>), BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var d = (StateDevice<StateType>)bindable;
                // d.State = (StateType)newValue;
                // slider.ValueChanged?.Invoke(slider, new ESliderValueChangeDelegate() (double)oldValue, (double)newValue));
            }); 


        // private StateType state;
        [JsonProperty(PropertyName = "State")]
        public StateType State
        {
            get { return (StateType)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        } */

        private StateType state;
        [JsonProperty(PropertyName = "State")]
        public StateType State
        {
            get { return state; }
            set { state = value; OnPropertyChanged("State"); }
        } 


        public StateDevice()
        {

        }

        public StateDevice(string name, StateType state): base (UIDID.NewID(), name, null)
        {
            State = state;
        }

    }
}
