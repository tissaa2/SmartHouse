namespace SmartHouse.Models.Logic
{
    public class DeviceState : BaseEntity<int>
    {
        public string Value { get; set; }

        public DeviceState()
        {

        }

        public DeviceState (int id, string value): base(id)
        {
            Value = value;
        }
    }
}
