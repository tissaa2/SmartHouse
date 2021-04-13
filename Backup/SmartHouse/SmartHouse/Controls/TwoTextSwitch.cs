using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHouse.Controls
{
    public class TwoTextSwitch: Switch
    {
        //public static readonly BindableProperty TextOnProperty = BindableProperty.Create("TextOn",
        //returnType: typeof(string),
        //declaringType: typeof(TwoTextSwitch),
        //defaultValue: "On",
        //propertyChanged: CaptionChanged, defaultBindingMode: BindingMode.OneWay);

        //public string TextOn
        //{
        //    get { return (string)GetValue(TextOnProperty); }
        //    set { SetValue(TextOnProperty, value); }
        //}

        //public static void CaptionChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    (bindable as ESlider).CaptionLabel.Text = (string)newValue;
        //}

        public string TextOn { get; set; } = "On";
        public string TextOff { get; set; } = "Off";

    }
}
