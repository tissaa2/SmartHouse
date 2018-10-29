using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHouse.Controls
{
    public class ESocketSwitch: Switch
    {

        public static readonly BindableProperty DataProperty = BindableProperty.Create("Data", typeof(object), typeof(ImageButton));
        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }


    }
}
