using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHouse.Controls
{
    public class Coordinates
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class TouchEventArgs : EventArgs
    {
        public Coordinates TouchPosition { get; set; }
    }

    public delegate void TouchEventDelegate(object sender, TouchEventArgs args);

    public class EFrame: Frame
    {
        public Coordinates LastTouchPosition { get; set; }
        public void ProcessTouchEvent(TouchEventArgs args)
        {
            LastTouchPosition = args.TouchPosition;
            Touched?.Invoke(this, args);
        }
        public event TouchEventDelegate Touched;
    }
}
