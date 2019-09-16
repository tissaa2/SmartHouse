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

    public class PDeviceType
    {
        public int ID {get; set;}
        public string Name { get; set; }
        public PDeviceType(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    public class DeviceModel : ViewModel
    {

        public static List<PDeviceType> InputDeviceTypes = new List<PDeviceType>()
        {
            new PDeviceType(0, "Выключатель(вкл/выкл)"),
            new PDeviceType(1, "Кнопка(выкл/вкл)"),
            new PDeviceType(2, "Датчик движения"),
            new PDeviceType(3, "Выключатель зоны"),
            new PDeviceType(4, "Выключатель всего"),
            new PDeviceType(5, "Кнопка(вкл/выкл)"),
            new PDeviceType(6, "Геркон"),
            new PDeviceType(7, "Кнопка(вкл/выкл деактивация)"),
            new PDeviceType(8, "Кнопка(вкл/выкл с задержкой)"),
            new PDeviceType(9, "Контроллер охраны"),
            new PDeviceType(10, "Кнопка управления шторами"),
            new PDeviceType(11, "Кнопка(вкл)"),
            new PDeviceType(12, "Кнопка(вкл/выкл) с двойным нажатием")
        };

        public static List<PDeviceType> OutputDeviceTypes = new List<PDeviceType>()
        {
            new PDeviceType(0, "Димируемый выход"),
            new PDeviceType(1, "Релейный выход")
        };

        public static List<EntityInfo> logicDeviceTypes = BaseEntity<int>.GetInheritors(typeof(Device));

        public List<PDeviceType> PhysicDeviceTypes
        {
            get
            {
                if (IsInput)
                        return InputDeviceTypes;
                    else
                        return OutputDeviceTypes;
            }
        }
        public List<EntityInfo> LogicDeviceTypes { get { return logicDeviceTypes; } }

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
            get
            {
                if (deviceType == null)
                    deviceType = logicDeviceTypes[typeID];
                return deviceType;
            }
            set
            {
                deviceType = value;
                if (value != null)
                {
                    typeID = value.ID;
                    OnPropertyChanged("DeviceType");
                }
            }
        }

        public Group Group { get; set; }

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged("ID"); /* device = null; OnPropertyChanged("Device"); */}
        }

        private string uid;
        public string UID
        {
            get { return uid; }
            set { uid = value; OnPropertyChanged("UID"); /* device = null; OnPropertyChanged("Device"); */}
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

        private string portID;
        [JsonProperty(PropertyName = "PortID")]
        public string PortID
        {
            get { return portID; }
            set { portID = value; OnPropertyChanged("PortID"); }
        }

        public bool IsInput { get; set; } 

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
                    // UID u = new UID(uid);
                    device = Group.Devices.FirstOrDefault(e => e.ID == id);
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
                this.uid = sm.uid;
                this.device = sm.device;
                this.deviceType = sm.deviceType;
                this.typeID = sm.typeID;
                this.securityLevel = sm.securityLevel;
                this.name = sm.name;
                this.portID = sm.PortID;
                this.IsInput = sm.IsInput;
            }
        }

        public void ApplyState(string state)
        {
            if (Device is DoubleStateDevice)
                (Device as DoubleStateDevice).ApplyState(state);
            else
            if (Device is BoolStateDevice)
                (Device as BoolStateDevice).ApplyState(state);
        }

        public override void Setup(params object[] args)
        {
            if (Target is Device)
            {
                var d = Target as Device;
                this.uid = (string)d.UID;
                this.device = d;
                this.name = d.Name;
                this.securityLevel = d.SecurityLevel;
                this.portID = d.PortID.ToString();
                this.IsInput = d.IsInput;
                if (args[0] is Type)
                {
                    var t = args[0] as Type;
                    DeviceType = LogicDeviceTypes.FirstOrDefault(e => e.Type.Name == t.Name);
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
                var i = Group.Devices.IndexOf(od);
                if (i > -1)
                    Group.Devices[i] = device;
            }
            device.ID = id;
            device.UID = new UID(uid);
            int v;
            if (int.TryParse(PortID, out v))
                device.PortID = (byte)v;
            device.Name = Name;
            device.SecurityLevel = SecurityLevel;
        }

        public DeviceModel() : base()
        {

        }
    }
}
