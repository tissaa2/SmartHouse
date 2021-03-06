﻿using System.Linq;
using SmartHouse.Models.Logic;
using SmartHouse.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using System;
using Device = SmartHouse.Models.Logic.Device;
using System.Collections.Generic;

namespace SmartHouse.ViewModels
{
    public class PPortType
    {
        public byte ID {get; set;}
        public string Name { get; set; }
        public PPortType(byte id, string name)
        {
            ID = id;
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class DeviceModel : NamedModel
    {

        public static List<PPortType> InputPortTypes = new List<PPortType>()
        {
            new PPortType(0, "Выключатель(вкл/выкл)"),
            new PPortType(1, "Кнопка(выкл/вкл)"),
            new PPortType(2, "Датчик движения"),
            new PPortType(3, "Выключатель зоны"),
            new PPortType(4, "Выключатель всего"),
            new PPortType(5, "Кнопка(вкл/выкл)"),
            new PPortType(6, "Геркон"),
            new PPortType(7, "Кнопка(вкл/выкл деактивация)"),
            new PPortType(8, "Кнопка(вкл/выкл с задержкой)"),
            new PPortType(9, "Контроллер охраны"),
            new PPortType(10, "Кнопка управления шторами"),
            new PPortType(11, "Кнопка(вкл)"),
            new PPortType(12, "Кнопка(вкл/выкл) с двойным нажатием")
        };

        public static List<PPortType> OutputPortTypes = new List<PPortType>()
        {
            new PPortType(0, "Димируемый выход"),
            new PPortType(1, "Релейный выход")
        };

        public static List<EntityInfo> logicDeviceTypes = BaseEntity.GetInheritors(typeof(Device), new Type[] { typeof(GroupSource) });

        public List<PPortType> PhysicPortTypes
        {
            get
            {
                if (IsInput)
                        return InputPortTypes;
                    else
                        return OutputPortTypes;
            }
        }
        public List<EntityInfo> LogicDeviceTypes { get { return logicDeviceTypes; } }

        private int typeID;

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
                CheckIsDirty(deviceType, value, "DeviceType", () => { deviceType = value; typeID = value == null ? -1 : value.ID; });
            }
        }

        private PPortType portType = null;
        public PPortType PortType
        {
            get
            {
                return portType;
            }
            set
            {
                CheckIsDirty(portType, value, "PortType", () => { 
                    portType = value; 
                    //if (value != null)
                    //    portTypeID = value.ID;
                });
            }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                CheckIsDirty(id, value, "ID", () => { id = value; });
            }
        }

        public Group Group { get; set; }

        private string uid;
        public string UID
        {
            get { return uid; }
            set
            {
                CheckIsDirty(uid, value, "UID", () => { uid = value; });
            }
        }

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                CheckIsDirty(enabled, value, "Enabled", () => { enabled = value; });
            }
        }

        private bool showDeleteButton = true;
        public bool ShowDeleteButton
        {
            get { return showDeleteButton && Settings.Instance.IsAdmin; }
            set
            {
                CheckIsDirty(showDeleteButton, value, "ShowDeleteButton", () => { showDeleteButton = value; });
            }
        }

        private string portID;
        [JsonProperty(PropertyName = "PortID")]
        public string PortID
        {
            get { return portID; }
            set
            {
                CheckIsDirty(portID, value, "PortID", () => { portID = value; });
            }
        }

        public bool IsInput { get; set; } 

        private byte securityLevel;
        [JsonProperty(PropertyName = "SecurityLevel")]
        public byte SecurityLevel
        {
            get { return securityLevel; }
            // set { securityLevel = value; OnPropertyChanged("SecurityLevel"); }
            set
            {
                CheckIsDirty(securityLevel, value, "SecurityLevel", () => { securityLevel = value; });
            }
        }

        public override void Assign(ViewModel source)
        {
            base.Assign(source);
            if (source is DeviceModel)
            {
                var sm = source as DeviceModel;
                this.uid = sm.uid;
                this.deviceType = sm.deviceType;
                this.typeID = sm.typeID;
                this.securityLevel = sm.securityLevel;
                this.name = sm.name;
                this.portID = sm.PortID;
                this.PortType = sm.PortType;
                this.IsInput = sm.IsInput;
            }
        }

        public void ApplyState(string state)
        {
            if (Target is DoubleStateDevice)
                (Target as DoubleStateDevice).ApplyState(state);
            else
            if (Target is BoolStateDevice)
                (Target as BoolStateDevice).ApplyState(state);
        }
        
        private PPortType GetPortType(int id, Device d)
        {
            if (d == null)
                return null;
            if (d.IsInput)
                return InputPortTypes.FirstOrDefault(e => e.ID == d.PortTypeID);
            else
                return OutputPortTypes.FirstOrDefault(e => e.ID == d.PortTypeID);
        }

        public override void Setup(params object[] args)
        {
            if (Target is Device)
            {
                var d = Target as Device;
                this.ID = d.ID;
                this.uid = (string)d.UID;
                this.name = d.Name;
                this.securityLevel = d.SecurityLevel;
                this.portID = d.PortID.ToString();
                this.IsInput = d.IsInput;
                this.portType = GetPortType(d.ID, d);
                if (args[0] is Type)
                {
                    var t = args[0] as Type;
                    DeviceType = LogicDeviceTypes.FirstOrDefault(e => e.Type.Name == t.Name);
                }
            }
            base.Setup();
        }

        public override void Apply(object target)
        {
            base.Apply(target);
            if (target is Device)
            {
                var device = target as Device;
                var od = device;
                device.ID = id;
                device.UID = new UID(uid);
                int v;
                if (int.TryParse(PortID, out v))
                    device.PortID = (byte)v;
                if (portType != null)
                    device.PortTypeID = portType.ID;
                device.SecurityLevel = SecurityLevel;
            }
        }

        public DeviceModel(Device source) : base()
        {
            this.Target = source;
            Setup();
        }

        public DeviceModel() : base()
        {
        }

        public override string ToString()
        {
            if (Target != null)
                return Target.ToString();
            else
                return "Не определено";
        }
    }
}
