using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    [IconName("device_lamp.png", "Светильник", false)]
    public class Lamp : DoubleStateDevice
    {
        public override string TypeName { get => "Светильник"; set => base.TypeName = value; }
        public override string Icon { get => "device_lamp.png"; set => base.Icon = value; }
        public override bool IsInput { get => false; set => base.IsInput = value; }

        public Lamp()
        {

        }

        public Lamp(int id, string name, double state, UID uid, byte portID) : base(id , name, state, uid, portID)
        {
            Init();
        }

        public override BaseEntity Clone()
        {
            return new Lamp() { ID = ID, UID = UID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
