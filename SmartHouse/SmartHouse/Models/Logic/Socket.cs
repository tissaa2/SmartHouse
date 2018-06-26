using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    public class Socket : Device
    {
        private bool enabled;
        [JsonProperty(PropertyName = "Enabled")]
        public bool Enabled
        {
            get { return enabled; }
            set { this.enabled = value; OnPropertyChanged("Enabled"); }
        }

        public override string Icon { get => "devices/socket.png"; set => base.Icon = value; }

        public Socket()
        {

        }

        public Socket(string name, bool enabled)
        {
            Name = name;
            Enabled = enabled;
        }
    }
}
