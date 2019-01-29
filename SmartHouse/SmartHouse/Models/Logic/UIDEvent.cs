using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using SmartHouse.Models.Physics;

namespace SmartHouse.Models.Logic
{
    public class UIDEvent: Event
    {
        public UID UID { get; set; }
        public UIDEvent(byte inputId, UID uid)
        {
            InputID = inputId;
            UID = uid;
        }

        public UIDEvent()
        {

        }
    }
}
