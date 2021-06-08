using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using SmartHouse.Models.Logic;
// using SmartHouse.Models.Physics;

namespace SmartHouse.Models.Logic
{
    public class Group : IconNamedEntity
    {
        public Project Project { get; set; }
        public List<Group> Children { get; set; } = new List<Group>();
        public List<int> DeviceIDs { get; set; } = new List<int>();
        public List<Scene> Scenes { get; set; } = new List<Scene>();

        [JsonIgnore]
        private Dictionary<int, Device> devices = null;
        [JsonIgnore]
        public Dictionary<int, Device> Devices
        {
            get
            {
                if (devices == null)
                {
                    devices = new Dictionary<int, Device>();    
                    foreach (var e in DeviceIDs)
                        devices.Add(e, Project.Devices[e]);
                }
                return devices;
            }
        }

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

            g.Items = new List<Scene>()
                                {
                                    new Scene(0,"Выключить все", "scene_switchoff.png",
                                         new UIDEvent(2, 2),
                                         new  List<DeviceState>(){ new DeviceState(1, "0")}, 0),
                                    new Scene(1, "Полный свет", "scene_brightlight.png",
                                         new UIDEvent(3, 3),
                                         new  List<DeviceState>(){new DeviceState(1, "100")}, 100)
                                };
            return g;
        }

        private void Setup()
        {
        }

        public Group()
        {
            Setup();
        }

        public Group(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
            Setup();
        }

        public override void Init()
        {
            base.Init();
            foreach (var e in Items)
            {
                e.Init();
            }
        }


    }
}
