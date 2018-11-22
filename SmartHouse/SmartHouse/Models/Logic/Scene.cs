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
                if (d is DoubleStateDevice)
                    (d as DoubleStateDevice).ApplyState(i.Value);
                else
                if (d is BoolStateDevice)
                    (d as BoolStateDevice).ApplyState(i.Value);
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

        private static Random vr = new Random();

        public Scene(UID id, string nameTemplate, string icon, Event _event, IEnumerable<Device> devices): base (id, nameTemplate, icon)
        {
            var r = new Random();
            Event = _event;
            var f = nameTemplate == "Выключить все";
            foreach (var i in devices)
            {
                if (f || r.Next(2) == 1)
                {
                    if (i is Socket)
                    {

                    }

                    string v = i is Socket ? "true" : vr.Next(100).ToString();
                    if (f)
                        v = i is Socket ? "false" : "0";
                    else
                        v = i is Socket ? "true" : vr.Next(100).ToString();

                    var e = new DeviceState() { ID = i.ID, SecurityLevel = i.SecurityLevel, Value = v };
                    Items.Add(e);
                }
            }
        }

    }
}
