using Newtonsoft.Json;
using SmartHouse.ViewModels;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{

    [IconName("device_groupsource.png", "Группа", true)]
    public class GroupSource : BoolStateDevice
    {
        public override string TypeName { get => "Группа"; set => base.TypeName = value; }
        public override string Icon { get => "device_panel.png"; set => base.Icon = value; }
        public override bool IsInput { get => true; set => base.IsInput = value; }

        public GroupSource()
        {
            ID = 0xFF00FF;
            Name = "Группа";
            // ToString();
        }

        public override BaseEntity Clone()
        {
            return new Switch() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
