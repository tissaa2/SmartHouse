using Newtonsoft.Json;
using System.Collections.ObjectModel;
using SmartHouse.Models.Physics;
using SmartHouse.Models.Packets;

namespace SmartHouse.Models.Logic
{
    // public class Device : IconEntity<int>
    public class Device : IconEntity
    {
        public virtual string TypeName { get; set; }

        private byte portID = 0;
        public byte PortID {
            get => portID;
            set
            {
                CheckIsDirty(portID, value, "PortID", () => { portID = value; });
            }
        }

        [JsonIgnore]
        private byte portTypeID = 0;
        public byte PortTypeID
        {
            get => portTypeID;
            set
            {
                CheckIsDirty(portTypeID, value, "PortTypeID", () => { portTypeID = value; });
            }
        }

        public UID uid;
        public UID UID
        {
            get => uid;
            set
            {
                CheckIsDirty(uid, value, "UID", () => { uid = value; });
            }
        }

        public virtual bool IsInput { get; set; }

        /* private bool _checked;
        public bool Checked {
            get {
                return _checked;
            }
            set {
                _checked = value;
                OnPropertyChanged("Checked");
            }
        } */

        /* private bool enabled;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
            }
        } */

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

        public virtual void SetState(DeviceState state)
        {

        }

    }
}
