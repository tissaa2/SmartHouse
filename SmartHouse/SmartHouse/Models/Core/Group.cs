using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Core;
using SmartHouse.Models.CAN;

namespace SmartHouse.Models.Core
{
    public class Group: IconListEntity<int, UID, Scene>
    {
        public static Group Kitchen()
        {
            return new Group() { Name = "Кухня", ID = 1, Icon = "kitchen.png", SecurityLevel = 0, Items = new List<Scene>() { } };
        }
    }
}
