using Newtonsoft.Json;
using System.Collections.ObjectModel;
using SmartHouse.Models.Physics;
using SmartHouse.Models.Packets;

namespace SmartHouse.Models.Logic
{
    public class Device : NamedIconEntity
    {
        public virtual string TypeName { get; set; }

        public byte PortID { get; set; }

        public byte PortTypeID { get; set; }

        public UID UID { get; set; }

        public virtual bool IsInput { get; set; }

        public Device()
        {
        }

        public Device(int id, UID uid, byte portID, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
            this.UID = uid;
            this.PortID = portID;
        }

        public virtual void ApplyState(string state)
        {

        }

        public virtual DeviceState GetState()
        {
            return null;
        }

    }
}
