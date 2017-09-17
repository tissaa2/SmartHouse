using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.CAN;
using System.Xml.Serialization;

namespace SmartHouse.Models.Scenes
{
    public class GroupOld: IEnumerable<Device>
    {
        public List<UID> UIDs;
        public int ID { get; set; }

        [XmlIgnore]
        protected Dictionary<UID, Device> devices = null;

        protected void CheckDevices()
        {
            if (devices == null)
            {
                devices = new Dictionary<UID, Device>();
                foreach (UID id in UIDs)
                {
                    devices.Add(id, Device.GetDevice(id).Clone());
                }
            }
        }

        public Device this[UID id]
        {
            get
            {
                CheckDevices();
                return devices[id];
            }
        }

        public IEnumerator<Device> GetEnumerator()
        {
            CheckDevices();
            return devices.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return devices.Values.GetEnumerator();
        }
    }
}
