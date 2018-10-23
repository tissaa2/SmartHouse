using System.Linq;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;

namespace SmartHouse.ViewModels
{

    public class DeviceModel: ViewModel 
    {
        public Group Group { get; set; }

        private UID id;
        [JsonProperty(PropertyName = "ID")]
        public UID ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Device"); device = null; }
        }

        private bool enabled;
        [JsonProperty(PropertyName = "Enabled")]
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; OnPropertyChanged("Enabled"); }
        }

        private Device device = null;
        [JsonIgnore]
        public Device Device
        {
            get
            {
                if (device == null)
                    device = Group.Devices.FirstOrDefault(e => e.ID == id);
                return device;
            }
        }

        public override void Assign(ViewModel source)
        {
            base.Assign(source);
            if (source is DeviceModel)
            {
                var sm = source as DeviceModel;
                this.Group = sm.Group;
                this.id = sm.id;
                this.device = sm.device;
            }
        }

        public override void Setup()
        {
            if (Target is Device)
            {
                var d = Target as Device;
                this.ID = d.ID;
                this.device = d;
            }
            base.Setup();
        }

        public DeviceModel(): base()
        {
        }
    }
}
