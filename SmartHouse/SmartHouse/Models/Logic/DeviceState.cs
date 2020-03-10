namespace SmartHouse.Models.Logic
{
    // public class DeviceState : BaseEntity<int>
    public class DeviceState : BaseEntity
    {
        public string value = null;
        public string Value
        {
            get => value;
            set
            {
                CheckIsDirty(this.value, value, "Value", () => { this.value = value; });
            }
        }

        public DeviceState()
        {

        }

        public DeviceState (int id, string value): base(id)
        {
            Value = value;
        }
    }
}
