using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.Physics;
using System.Linq;

namespace SmartHouse.Models.Logic
{
    public class UIDEvent: Event
    {
        // public UID UID { get; set; }
        // public UIDEvent(byte inputId, UID uid)
        private int deviceID;
        public int DeviceID {
            get => deviceID;
            set
            {
                CheckIsDirty(deviceID, value, "DeviceID", () => { deviceID = value; });
            }
        }

        public UIDEvent(byte inputId, int deviceId)
        {
            InputID = inputId;
            DeviceID = deviceId;
            Init();
            // UID = uid;
        }

        public UIDEvent()
        {

        }

        public override UID GetUID(Group group)
        {
            var device = group.Devices.FirstOrDefault(e => e.ID == DeviceID);
            if (device != null)
                return device.UID;
            return base.GetUID(group);
        }
    }
}
