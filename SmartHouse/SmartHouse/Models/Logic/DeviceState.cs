namespace SmartHouse.Models.Logic
{
    public class DeviceState : BaseEntity<UID>
    {
        public string Value { get; set; }

        public DeviceState()
        {

        }

        public DeviceState (UID id, string value): base(id)
        {
            Value = value;
        }
    }
}
