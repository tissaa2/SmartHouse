using System.Linq;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using System;
using Device = SmartHouse.Models.Logic.Device;
using System.Collections.Generic;

namespace SmartHouse.ViewModels
{
    /* public class DeviceType
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public DeviceType(int id, string name)
        {
            Name = name;
            ID = id;
        }
    } */

    public class DeviceModel: ViewModel 
    {

        public static List<EntityInfo> deviceTypes = BaseEntity<int>.GetInheritors(typeof(Device));

        public List<EntityInfo> DeviceTypes { get { return deviceTypes; } }

        public static readonly BindableProperty TypeIDProperty = BindableProperty.Create("TypeID", typeof(int), typeof(DeviceModel), default(int));
        private int typeID;
        [JsonProperty(PropertyName = "TypeID")]
        public int TypeID
        {
            get { return typeID; }
            set { deviceType = null; this.typeID = value; OnPropertyChanged("TypeID"); SetValue(TypeIDProperty, value); }
        }

        private EntityInfo deviceType = null;
        public EntityInfo DeviceType
        {
            get {
                if (deviceType == null)
                    deviceType = deviceTypes[typeID];
                return deviceType;
            }
            set {
                deviceType = value;
                if (value != null)
                {
                    typeID = value.ID;
                    OnPropertyChanged("DeviceType");
                }
            }
        }

        public Group Group { get; set; }



        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged("ID"); /* device = null; OnPropertyChanged("Device"); */}
        }

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; OnPropertyChanged("Enabled"); }
        }

        private bool showDeleteButton = true;
        public bool ShowDeleteButton
        {
            get { return showDeleteButton && Device.IsAdmin; }
            set { showDeleteButton = value; OnPropertyChanged("ShowDeleteButton"); }
        }

        private string name;
        [JsonProperty(PropertyName = "Name")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private byte securityLevel;
        [JsonProperty(PropertyName = "SecurityLevel")]
        public byte SecurityLevel
        {
            get { return securityLevel; }
            set { securityLevel = value; OnPropertyChanged("SecurityLevel"); }
        }

        private Device device = null;
        [JsonIgnore]
        public Device Device
        {
            get
            {
                if (device == null)
                {
                    UID uid = new UID(id);
                    device = Group.Devices.FirstOrDefault(e => e.ID == uid);
                }
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
                this.deviceType = sm.deviceType;
                this.typeID = sm.typeID;
                this.securityLevel = sm.securityLevel;
                this.name = sm.name;
            }
        }

        public override void Setup(params object[] args)
        {
            if (Target is Device)
            {
                var d = Target as Device;
                this.id = (string)d.ID;
                this.device = d;
                this.name = d.Name;
                this.securityLevel = d.SecurityLevel;
                if (args[0] is Type)
                {
                    var t = args[0] as Type;
                    DeviceType = DeviceTypes.FirstOrDefault(e => e.Type.Name == t.Name);
                }
            }
            base.Setup();
        }

        public void Apply()
        {
            var od = device;
            if (deviceType.Type.Name != device.GetType().Name)
            {
                device = deviceType.CreateEntity() as Device;
                device.ID = new UID(id);
                var i = Group.Devices.IndexOf(od);
                if (i > -1)
                    Group.Devices[i] = device;
            }
            device.Name = Name;
            device.SecurityLevel = SecurityLevel;
        }

        public DeviceModel(): base()
        {

        }
    }
}
