using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{

    [IconName("device_switch.png", "Выключатель", true)]
    public class Switch : BoolStateDevice
    {
        public override string TypeName { get => "Выключатель"; set => base.TypeName = value; }
        public override string Icon { get => "device_panel.png"; set => base.Icon = value; }
        public override bool IsInput { get => true; set => base.IsInput = value; }

        public Switch()
        {

        }

        public Switch(int id, string name, bool state, UID uid, byte portID) : base(id, name, state, uid, portID)
        {
        }

        public override BaseEntity<int> Clone()
        {
            return new Switch() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
