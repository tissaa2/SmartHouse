using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Storage;
using SmartHouse.Services;
using SmartHouse.Models.Packets;
using Xamarin.Forms;

namespace SmartHouse.ViewModels.Devices.Physic
{
    public class PortModel : IconNamedModel
    {
        public Stack<double> previousValues = new Stack<double>();
        public double value = 0;
        public double Value { get => value ; set => SetValue(value); }
        public Color bgColor = Color.Transparent;
        public Color BGColor { get => bgColor; set { bgColor = value; OnPropertyChanged("BGColor"); } }

        public PortModel(int id)
        {
            ID = id;
        }

        public void PushValue()
        {
            previousValues.Push(Value);
        }

        public void PopValue()
        {
            Value = previousValues.Pop();
        }

        public virtual void SetValue(double val)
        {
            SetLocalValue(val);

            if (Parent is PhysicDeviceModel)
            {
                var p = Parent as PhysicDeviceModel;
                CAN.Instance.SetPortValue(p.UID, ID, (byte)value, 1);
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
