using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHouse.Controls
{
    public class TouchEventArgs: EventArgs
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public delegate void TouchEventDelegate(object sender, TouchEventArgs args);

    public class EFrame: Frame

    {
        public void CallTouchEvent(TouchEventArgs args)
        {
            Touched?.Invoke(this, args);
        }
        public event TouchEventDelegate Touched;
    }
}
