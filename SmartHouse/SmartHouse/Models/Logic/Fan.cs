using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    [IconName("device_fan.png", "Вентилятор")]
    public class Fan : DoubleStateDevice
    {
        public override string TypeName { get => "Вентилятор"; set => base.TypeName = value; }
        public override string Icon { get => "device_fan.png"; set => base.Icon = value; }

        public Fan()
        {

        }

        public Fan(string name, double state): base(name, state)
        {
        }

        public override BaseEntity<UID> Clone()
        {
            return new Fan() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
