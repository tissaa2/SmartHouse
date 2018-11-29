﻿using Newtonsoft.Json;
using SmartHouse.Models.Physics;
using System;

namespace SmartHouse.Models.Logic
{
    public class DoubleStateDevice : /* StateDevice<double> */ Device 
    {

        private double state;
        [JsonProperty(PropertyName = "State")]
        public double State
        {
            get { return state; }
            set
            {
                state = value > 90 ? 100 : value < 10 ? 0 : value;
                Port.SetPortValue(ID, PortID, (byte)state);
                OnPropertyChanged("State");
            }
        }

        public DoubleStateDevice()
        {

        }

        public DoubleStateDevice(string name, double state): base (UIDID.NewID(), name, null)
        {
            State = state;
        }

        public override void ApplyState(string state)
        {
            double v;
            if (double.TryParse(state, out v))
            {
                State = v;
            }
        }

        public override void SetState(DeviceState state)
        {
            state.Value = State.ToString();
        }

    }
}
