using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Logic;
using SmartHouse.Models.Physics;

namespace SmartHouse.Models.Logic
{
    public class Group : IconListEntity<int, UID, Scene>
    {
        public List<Group> Children { get; set; }

        public static Group Create(string name, string icon, int id)
        {
            int bid = id * 10;
            return new Group()
            {
                Name = name,
                Icon = icon,
                ID = id,
                Items = new List<Scene>()
                                {
                                    Scene.LightsOff(1 + bid),
                                    Scene.Night(2 + bid),
                                    Scene.SoftLight(3 + bid),
                                    Scene.BrightLight(4 + bid)
                                    /* new GroupEvent() { CategoryID = 1, GroupID = 1, Icon = "workLight.png", InputID = 0, Name = "Рабочий свет", SecurityLevel = 0, TimePar = 0},
                                    new UIDEvent() { Icon = "socket.png", InputID = 0, Name = "Розетка плиты", SecurityLevel = 1} */
                                }
            };
        }

    }
}
