using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using SmartHouse.Models.Storage;
// using SmartHouse.Models.Physics;

namespace SmartHouse.Models.Storage
{
    public class Group : IconNamedEntity
    {
        public Project Project { get; set; }
        public List<Group> Children { get; set; } = new List<Group>();
        public List<int> DeviceIDs { get; set; } = new List<int>();
        public List<Scene> Scenes { get; set; } = new List<Scene>();

        public static Group Create(Project parent, string name, string icon, int id)
        {
            var g = new Group()
            {
                Project = parent,
                Name = name,
                Icon = icon,
                ID = id,
                DeviceIDs = new List<int>(parent.Devices.Keys)
            };

            var d2 = parent.Devices[2];
            var d3 = parent.Devices[3];

            g.Scenes = new List<Scene>()
                                {
                                    new Scene("Выключить все", "scene_switchoff.png",
                                         Event.UIDEvent(d2.UID, 2, 0),
                                         new  List<DeviceState>(){ new DeviceState(1, "0")}, 0),
                                    new Scene("Полный свет", "scene_brightlight.png",
                                         Event.UIDEvent(d3.UID, 3, 0),
                                         new  List<DeviceState>(){new DeviceState(1, "100")}, 100)
                                };
            return g;
        }

        public Group()
        {
        }

        public Group(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
        }

    }
}
