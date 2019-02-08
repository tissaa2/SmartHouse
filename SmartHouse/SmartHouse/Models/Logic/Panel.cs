using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{

    [IconName("device_panel.png", "Кнопочная панель")]
    public class Panel : MultyBoolStateDevice
    {
        public override string TypeName { get => "Кнопочная панель"; set => base.TypeName = value; }
        public override string Icon { get => "device_panel.png"; set => base.Icon = value; }
        public override bool IsInput { get => true; set => base.IsInput = value; }

        public Panel()
        {

        }

        public Panel(int id, string name, IEnumerable<bool> state, UID uid, byte portID) : base(id, name, state, uid, portID)
        {
        }

        public override BaseEntity<int> Clone()
        {
            return new Panel() { ID = ID, UID = UID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
