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

        public override string Icon { get => "device_fan.png"; set => base.Icon = value; }

        public Fan()
        {

        }

        public Fan(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public override BaseEntity<UID> Clone()
        {
            return new Fan() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, Value = Value };
        }


    }
}
