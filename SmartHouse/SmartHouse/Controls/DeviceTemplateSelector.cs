using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Logic;
using Xamarin.Forms;
using SmartHouse.ViewModels;
using SmartHouse.Models;


namespace SmartHouse.Controls
{
    public class DeviceTemplateSelector : DataTemplateSelector
    {
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
            }
            return DefaultTemplate;
        }

        public DeviceTemplateSelector()
        {

        }

    }
}
