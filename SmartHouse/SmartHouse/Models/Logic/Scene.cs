using SmartHouse.Models.Physics;
using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    // public class Scene : IconListEntity<UID, UID, Device>
    public class Scene : IconEntity<UID>
    {
        /*                            
                                    new Scene() { ID = new UID(1), 
        */
        public static Scene LightsOff(int uid)
        {
            return new Scene() { ID = new UID(uid), Name = "Выключить все", Event = new GroupEvent(0, 1, 0, 0), Icon = "scene_switchoff.png" };
        }

        public static Scene SoftLight(int uid)
        {
            return new Scene() { ID = new UID(uid), Name = "Мягкий свет", Event = new GroupEvent(0, 1, 0, 0), Icon = "scene_softlight.png" };
        }

        public static Scene BrightLight(int uid)
        {
            return new Scene() { ID = new UID(uid), Name = "Полный свет", Event = new GroupEvent(0, 1, 0, 0), Icon = "scene_brightlight.png" };
        }

        public static Scene Night(int uid)
        {
            return new Scene() { ID = new UID(uid), Name = "Ночной свет", Event = new GroupEvent(0, 1, 0, 0), Icon = "scene_nightlight.png" };
        }

        public void Activate()
        {

        }

        [JsonIgnore]
        public Event Event { get; set; }
        // public string Event { get; set; }

        public Scene()
        {

        }

        public Scene(int id, string nameTemplate, string icon): base (id, nameTemplate, icon)
        {
            
        }
    }
}
