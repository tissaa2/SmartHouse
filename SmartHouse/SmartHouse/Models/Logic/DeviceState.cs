namespace SmartHouse.Models.Logic
{
    // public class DeviceState : BaseEntity<int>
    public class DeviceState : BaseEntity
    {
        public int DeviceID { get; set; }
        public string Value { get; set; } = null;

        public DeviceState()
        {

        }

        public DeviceState (int id, string value): base(id)
        {
            Value = value;
        }
    }
}
