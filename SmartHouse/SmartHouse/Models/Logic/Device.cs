using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    public class Device : IconEntity<UID>
    {
        public Device()
        {

        }

        public Device(UID id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {

        }
    }
}
