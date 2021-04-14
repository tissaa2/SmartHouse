using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Logic;
using SmartHouse.Services;
using SmartHouse.Models.Packets;
using Xamarin.Forms;

namespace SmartHouse.Models.Physics
{
    // public class Port : IconEntity<int>
    public class Port : NamedIconEntity
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

        public static void SetPortValue(UID deviceId, int portId, byte value, byte fadeTime)
        {
            // return;
            if (Client.CurrentServer == null)
                return;
            if (deviceId.Hash == 0)
                return;
            //Packet.SetOutputValueRequest[7] = deviceId.B2;
            //Packet.SetOutputValueRequest[8] = deviceId.B1;
            //Packet.SetOutputValueRequest[9] = deviceId.B0;
            //Packet.SetOutputValueRequest[11] = (byte)portId;
            //Packet.SetOutputValueRequest[12] = (byte)value;
            //Packet.SetOutputValueRequest[13] = fadeTime;
            var data = Packet.CreateSetOutputValueRequest(deviceId, (byte)portId, value, 0, fadeTime, 0, 0);
            Client.CurrentServer.SendToCAN(data, 20000, Packet.GetControllerCommand(data), Packet.GetCANCommand(data), deviceId, (p, e) => { return e; }, (p, e) => { return e; });
        }

        // 2DO: предусмотреть booleanPort и analogPort

        public virtual void SetValue(double val)
        {
            SetLocalValue(val);

            if (Parent != null)
            {
                SetPortValue(Parent.ID, ID, (byte)value, 1);
            }
        }


        public virtual void SetLocalValue(double val)
        {

            if (val < 10)
                val = 0;
            if (val > 90)
                val = 100;

            value = val;

            OnPropertyChanged("Value");
        }
    }
}
