using Newtonsoft.Json;

namespace SmartHouse.Models.Storage
{
    public class DeviceState
    {
        public int DeviceID { get; set; }
        
        public string Value { get; set; } = null;

        public DeviceState()
        {

        }

        public DeviceState (int id, string value)
        {
            Value = value;
            DeviceID = id;
        }
    }
}
