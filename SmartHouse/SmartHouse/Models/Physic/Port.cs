using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Logic;
using SmartHouse.Services;
using SmartHouse.Models.Packets;
using Xamarin.Forms;

namespace SmartHouse.Models.Physics
{
    public class Port : IconEntity<int>
    {
        public Stack<double> previousValues = new Stack<double>();
        public PDevice Parent { get; set; }
        public double value = 0;
        public double Value { get => value ; set => SetValue(value); }
        public Color bgColor = Color.Transparent;
        public Color BGColor { get => bgColor; set { bgColor = value; OnPropertyChanged("BGColor"); } }
        

        public void PushValue()
        {
            previousValues.Push(Value);
        }

        public void PopValue()
        {
            Value = previousValues.Pop();
        }

        public virtual Port Clone()
        {
            return null;
        }

        public virtual void SetValue(double val)
        {
            //byte[] SetOutputValueRequest = new byte[]
            //{
            //36,     // 0
            //72,     // 1
            //76,     // 2
            //0,      // 3
            //0x30,   // 4
            //9,      // 5 data size
            //0x0C,   // 6 config byte
            //0,      // 7 UID3
            //0,      // 8 UID2
            //0,      // 9 UID1
            //0x54,   // 10 command 
            //0,      // 11 channel
            //0,      // 12 level (value)
            //0,      // 13 delay (in sec/4)
            //0,      // 14 fade time (in sec/4)
            //0,      // 15 GN (group number)
            //0       // 16 BN (button number)
            //};

            if (val < 10)
                val = 0;
            if (val > 90)
                val = 100;

            value = val;

            if (Parent != null)
            {
                Packet.SetOutputValueRequest[7] = Parent.ID.B2;
                Packet.SetOutputValueRequest[8] = Parent.ID.B1;
                Packet.SetOutputValueRequest[9] = Parent.ID.B0;
                Packet.SetOutputValueRequest[11] = (byte)ID;
                Packet.SetOutputValueRequest[12] = (byte)value;
                Client.CurrentServer.SendAndWaitForResponse(Packet.SetOutputValueRequest, 0x30, "set value", p => { });
            }


            OnPropertyChanged("Value");
        }
    }
}
