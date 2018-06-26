﻿using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    public class Lamp : Device
    {
        private double value;
        [JsonProperty(PropertyName = "Value")]
        public double Value
        {
            get { return value; }
            set { this.value = value; OnPropertyChanged("Value"); }
        }

        public override string Icon { get => "devices/lamp.png"; set => base.Icon = value; }

        public Lamp()
        {

        }

        public Lamp(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
