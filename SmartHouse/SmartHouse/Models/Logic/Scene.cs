using SmartHouse.Models.Physics;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace SmartHouse.Models.Logic
{
    public class Scene : IconListEntity<UID, UID, DeviceState>
    // public class Scene : IconListEntity<UID, UID, Device>
    // public class Scene : IconEntity<UID>
    {
        public static Scene LightsOff(int uid, Group group)
        {
            return new Scene(new UID(uid), "Выключить все", "scene_switchoff.png", new GroupEvent(0, (byte)group.ID, 0, 0), group.Devices);
        }

        public static Scene Night(int uid, Group group)
        {
            return new Scene(new UID(uid), "Ночной свет", "scene_nightlight.png", new GroupEvent(0, (byte)group.ID, 0, 0), group.Devices);
        }

        public static Scene BrightLight(int uid, Group group)
        {
            return new Scene(new UID(uid), "Полный свет", "scene_brightlight.png", new GroupEvent(0, (byte)group.ID, 0, 0), group.Devices);
        }

        public static Scene SoftLight(int uid, Group group)
        {
            return new Scene(new UID(uid), "Мягкий свет", "scene_softlight.png", new GroupEvent(0, (byte)group.ID, 0, 0), group.Devices);
        }

        public void Activate(Group group)
        {
            foreach (var i in Items)
            {
                var d = group.Devices.FirstOrDefault(e => e.ID == i.ID);
                if (d != null)
                    d.ApplyState(i.Value);
            }
        }

        [JsonIgnore]
        public Event Event { get; set; }
        // public string Event { get; set; }

        public Scene()
        {

        }

        public Scene(UID id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {

        }

        public Scene(UID id, string nameTemplate, string icon, Event _event, IEnumerable<Device> devices): base (id, nameTemplate, icon)
        {
            var r = new Random();
            Event = _event;
            var vr = new Random();
            foreach(var i in devices)
            {
                if (r.Next(2) == 1)
                {
                    var e = new DeviceState() { ID = i.ID, SecurityLevel = i.SecurityLevel, Value = i is Socket ? "true" : vr.Next(100).ToString() };
                    Items.Add(e);
                }
            }
        }

    }
}
