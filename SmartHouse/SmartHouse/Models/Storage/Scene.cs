using SmartHouse.Models.Physics;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using SmartHouse.Services;
using SmartHouse.Models.Packets;

namespace SmartHouse.Models.Storage
{
    public class Scene : IconNamedEntity
    {
        public List<DeviceState> States { get; set; }    

        public static Scene LightsOff(int id, Group group, List<Device> devices)
        {
            return new Scene(id, "Выключить все", "scene_switchoff.png", Event.GroupEvent(0, (byte)group.ID, 0, 0), devices);
        }

        public static Scene Night(int id, Group group, List<Device> devices)
        {
            return new Scene(id, "Ночной свет", "scene_nightlight.png", Event.GroupEvent(0, (byte)group.ID, 0, 0), devices);
        }

        public static Scene BrightLight(int id, Group group, List<Device> devices)
        {
            return new Scene(id, "Полный свет", "scene_brightlight.png", Event.GroupEvent(0, (byte)group.ID, 0, 0), devices);
        }

        public static Scene SoftLight(int id, Group group, List<Device> devices)
        {
            return new Scene(id, "Мягкий свет", "scene_softlight.png", Event.GroupEvent(0, (byte)group.ID, 0, 0), devices);
        }

        //public async void Activate(Group group)
        //{

        //    UID id = Event.GetUID(group);

        //    var ar = await Utils.P(Packet.CreateActivateSceneRequest(id, Event.InputID, Event.TypeID, 4, 0), 0);
        //    if (!ar)
        //        Log.Write("Error activating scene : sourceUID = {0} , sceneName = {1}, inputID = {2}", id, this.Name, this.Event.InputID);
        //}

        public Event Event { get; set; }

        public Scene()
        {

        }

        public Scene(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
            States = new List<DeviceState>();
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
                    var f0 = i.Type == DeviceType.Socket;
                    string v = f0 ? "true" : vr.Next(100).ToString();
                    if (f)
                        v = f0 ? "false" : "0";
                    else
                        v = f0 ? "true" : vr.Next(100).ToString();

                    var e = new DeviceState(i.ID, v);
                    States.Add(e);
                }
            }
        }

        public Scene(string name, string icon, Event _event, IEnumerable<DeviceState> deviceStates, double value) : base(ProjectsList.Instance.IntID.NewID(), name, icon)
        {
            Event = _event;
            States = new List<DeviceState>(deviceStates);
            foreach(var ds in States)
            {
                ds.Value = value.ToString();
            }
        }
    }
}
