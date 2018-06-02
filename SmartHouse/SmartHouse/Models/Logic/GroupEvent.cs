using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Logic
{
    public class GroupEvent: Event
    {
        public byte GroupID { get; set; }
        public byte CategoryID { get; set; }
        public byte TimePar { get; set; }

        public GroupEvent()
        {

        }

        public GroupEvent(byte inputId, byte groupId, byte categoryId, byte timePar)
        {
            InputID = inputId;
            GroupID = groupId;
            CategoryID = categoryId;
            TimePar = timePar;
        }
    }
}
