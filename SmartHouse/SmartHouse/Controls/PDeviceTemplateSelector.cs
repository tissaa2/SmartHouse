using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SmartHouse.ViewModels;
using SmartHouse.Models;
using SmartHouse.Models.Physics;

namespace SmartHouse.Controls
{
    public class PDeviceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DimmerTemplate { get; set; }
        public DataTemplate RelayTemplate { get; set; }
        public DataTemplate IRTrancieverTemplate { get; set; }
        public DataTemplate IRPanelTemplate { get; set; }
        public DataTemplate MSTPanelTemplate { get; set; }
        public DataTemplate MotionSensorTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Dimmer)
                return DimmerTemplate;
            if (item is Relay)
                return RelayTemplate;
            if (item is IRTranciever)
                return IRTrancieverTemplate;
            if (item is IRPanel)
                return IRPanelTemplate;
            if (item is MSTPanel)
                return MSTPanelTemplate;
            if (item is MotionSensor)
                return MotionSensorTemplate;
            return DefaultTemplate;
        }

        public PDeviceTemplateSelector()
        {

        }

    }
}
