using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    [IconName("device_lamp.png", "Светильник")]
    public class Lamp : DoubleStateDevice
    {
        public override string TypeName { get => "Светильник"; set => base.TypeName = value; }
        public override string Icon { get => "device_lamp.png"; set => base.Icon = value; }

        public Lamp()
        {

        }

        public Lamp(string name, double state): base(name, state)
        {
        }

        public override BaseEntity<UID> Clone()
        {
            return new Lamp() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
