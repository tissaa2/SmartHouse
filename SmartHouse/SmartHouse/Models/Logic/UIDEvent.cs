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
        public int DeviceID { get; set; }
        public UIDEvent(byte inputId, int deviceId)
        {
            InputID = inputId;
            DeviceID = deviceId;
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
