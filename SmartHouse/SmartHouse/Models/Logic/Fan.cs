using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    [IconName("device_fan.png", "Вентилятор")]
    public class Fan : DoubleStateDevice
    {
        public override string TypeName { get => "Вентилятор"; set => base.TypeName = value; }
        public override string Icon { get => "device_fan.png"; set => base.Icon = value; }
        public override bool IsInput { get => false; set => base.IsInput = value; }

        public Fan()
        {

        }

        public Fan(int id, string name, double state, UID uid, byte portID) : base(id, name, state, uid, portID)
        {
        }

        public override BaseEntity<int> Clone()
        {
            return new Fan() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
