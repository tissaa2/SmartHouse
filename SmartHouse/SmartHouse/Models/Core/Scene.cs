using SmartHouse.Models.CAN;
using System.Collections.Generic;

namespace SmartHouse.Models.Core
{
    public class Scene: IconListEntity<UID, UID, Device>
    {

        public static Scene Day()
        {
            return new Scene() { ID = 0x00000001, Icon = "day.png", Name = "День", SecurityLevel = 0, Items = new List<Device>() { } };
        }

        public Event Event { get; set; }
    }
}
