using Newtonsoft.Json;
using System.Collections.ObjectModel;
using SmartHouse.Models.Physics;
using SmartHouse.Models.Packets;
using System.Collections.Generic;

namespace SmartHouse.Models.Storage
{
    public enum DeviceType
    {
        Lamp,
        Fan,
        MotionSensor,
        Panel,
        Socket,
        Switch,
        Group
    }

    public class Device : IconNamedEntity
    {
        public virtual DeviceType Type { get; set; }

        public byte PortID { get; set; }

        public byte PortTypeID { get; set; }

        public UID UID { get; set; }

        public virtual bool IsInput { get; set; }

        public Device()
        {
        }

        public Device(DeviceType deviceType, int id, UID uid, byte portID, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
            this.UID = uid;
            this.PortID = portID;
            this.Type = deviceType;
        }

    }
}
