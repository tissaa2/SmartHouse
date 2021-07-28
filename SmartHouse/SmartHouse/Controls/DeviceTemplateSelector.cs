using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Storage;
using Xamarin.Forms;
using SmartHouse.ViewModels;
using SmartHouse.Models;


namespace SmartHouse.Controls
{
    public class DeviceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SwitchTemplate { get; set; }
        public DataTemplate PanelTemplate { get; set; }
        public DataTemplate MotionSensorTemplate { get; set; }
        public DataTemplate FanTemplate { get; set; }
        public DataTemplate LampTemplate { get; set; }
        public DataTemplate SocketTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is DeviceModel)
            {
                var d = (item as DeviceModel).Device;
                if (d is Fan)
                    return FanTemplate;
                if (d is Lamp)
                    return LampTemplate;
                if (d is Socket)
                    return SocketTemplate;
                if (d is SmartHouse.Models.Storage.Switch)
                    return SwitchTemplate;
                if (d is Panel)
                    return PanelTemplate;
                if (d is MotionSensor)
                    return MotionSensorTemplate;
            }
            return DefaultTemplate;
        }

        public DeviceTemplateSelector()
        {

        }

    }
}
