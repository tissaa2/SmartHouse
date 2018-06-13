using SmartHouse.Models.Physics;
using System.Collections.Generic;

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
            return new Scene() { ID = new UID(uid), Name = "Выключить все", Event = new GroupEvent(0, 1, 0, 0), Icon = "switchoff.png" };
        }

        public static Scene SoftLight(int uid)
        {
            return new Scene() { ID = new UID(uid), Name = "Мягкий свет", Event = new GroupEvent(0, 1, 0, 0), Icon = "softlight.png" };
        }

        public static Scene BrightLight(int uid)
        {
            return new Scene() { ID = new UID(uid), Name = "Полный свет", Event = new GroupEvent(0, 1, 0, 0), Icon = "brightlight.png" };
        }

        public static Scene Night(int uid)
        {
            return new Scene() { ID = new UID(uid), Name = "Ночной свет", Event = new GroupEvent(0, 1, 0, 0), Icon = "nightlight.png" };
        }

        public void Activate()
        {

        }

        public Event Event { get; set; }
        // public string Event { get; set; }
    }
}
