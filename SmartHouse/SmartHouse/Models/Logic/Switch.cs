using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{

    [IconName("device_switch.png", "Выключатель")]
    public class Switch : BoolStateDevice
    {
        public override string TypeName { get => "Кнопочная панель"; set => base.TypeName = value; }
        public override string Icon { get => "device_panel.png"; set => base.Icon = value; }
        public override bool IsInput { get => true; set => base.IsInput = value; }

        public Switch()
        {

        }

        public Switch(string name, bool state): base(name, state)
        {
        }

        public override BaseEntity<UID> Clone()
        {
            return new Switch() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
