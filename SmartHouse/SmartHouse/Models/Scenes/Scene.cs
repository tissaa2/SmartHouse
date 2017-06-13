using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.CAN;
using System.Xml.Serialization;

namespace SmartHouse.Models.Scenes
{
    public class Scene: IEnumerable<Device>
    {
        public int ID { get; set; }
        public List<Device> Devices;

        [XmlIgnore]
        protected Dictionary<UID, Device> index = null;

        protected void CheckDevices()
        {
            if (index == null)
            {
                index = new Dictionary<UID, Device>();
                foreach (Device d in Devices)
                {
                    index.Add(d.ID, d);
                }
            }
        }

        public Device this[UID id]
        {
            get
            {
                CheckDevices();
                return Device.Devices[id];
            }
        }

        public IEnumerator<Device> GetEnumerator()
        {
            CheckDevices();
            return index.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return index.Values.GetEnumerator();
        }
    }
}
