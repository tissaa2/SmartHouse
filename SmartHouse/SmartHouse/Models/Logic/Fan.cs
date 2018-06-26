using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    public class Fan : Device
    {
        private double value;
        [JsonProperty(PropertyName = "Value")]
        public double Value
        {
            get { return value; }
            set { this.value = value; OnPropertyChanged("Value"); }
        }

        public override string Icon { get => "devices/fan.png"; set => base.Icon = value; }
    }
}
