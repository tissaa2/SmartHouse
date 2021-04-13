using SmartHouse.Models.Physics;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using SmartHouse.Services;
using SmartHouse.Models.Packets;

namespace SmartHouse.Models.Logic
{
    public class Scene : IconListEntity<int, DeviceState>
    {

        public static Scene LightsOff(int id, Group group)
        {
            return new Scene(id, "Выключить все", "scene_switchoff.png", new GroupEvent(0, (byte)group.ID, 0, 0), group.Devices);
        }

        public static Scene Night(int id, Group group)
        {
            return new Scene(id, "Ночной свет", "scene_nightlight.png", new GroupEvent(0, (byte)group.ID, 0, 0), group.Devices);
        }

        public static Scene BrightLight(int id, Group group)
        {
            return new Scene(id, "Полный свет", "scene_brightlight.png", new GroupEvent(0, (byte)group.ID, 0, 0), group.Devices);
        }

        public static Scene SoftLight(int id, Group group)
        {
            return new Scene(id, "Мягкий свет", "scene_softlight.png", new GroupEvent(0, (byte)group.ID, 0, 0), group.Devices);
        }

        public async void Activate(Group group)
        {

            // закомментил принудительное выставление параметров слайдеров. пусть работает обратная связь 
            //foreach (var i in Items)
            //{
            //    var d = group.Devices.FirstOrDefault(e => e.ID == i.ID);
            //    if (d is DoubleStateDevice)
            //        (d as DoubleStateDevice).ApplyState(i.Value);
            //    else
            //    if (d is BoolStateDevice)
            //        (d as BoolStateDevice).ApplyState(i.Value);
            //}


            // UID id = Event is UIDEvent ? (Event as UIDEvent).UID : new UID(0, 0, (Event as GroupEvent).GroupID);

            UID id = Event.GetUID(group);

            var ar = await Utils.P(Packet.CreateActivateSceneRequest(id, Event.InputID, Event.TypeID, 4, 0), 0);
            //ar.Wait();
            //if (!ar.Result)
            if (!ar)
                Log.Write("Error activating scene : sourceUID = {0} , sceneName = {1}, inputID = {2}", id, this.Name, this.Event.InputID);
        }

        // private Event _event = new UIDEvent(0, 0);
        private Event _event = null;
        public Event Event
        {
            get => _event;
            set
            {
                CheckIsDirty(_event, value, "Event", () => { _event = value; });
            }
        }

        // public Event Event { get; set; } = new UIDEvent(0, new UID(0));
        // public string Event { get; set; }

        public Scene()
        {

        }

        public Scene(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
            Init();
        }

        private static Random vr = new Random();

        public Scene(int id, string nameTemplate, string icon, Event _event, IEnumerable<Device> devices): base (id, nameTemplate, icon)
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

        public Scene(string name, string icon, Event _event, IEnumerable<DeviceState> deviceStates, double value) : base(Scene.IntID.NewID(), name, icon)
        {
            Event = _event;
            Items = new List<DeviceState>(deviceStates);
            foreach(var ds in Items)
            {
                ds.Value = value.ToString();
            }
        }

    }
}
