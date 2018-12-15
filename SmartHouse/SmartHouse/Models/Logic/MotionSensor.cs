using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{

    [IconName("device_motionsensor.png", "Датчик движения")]
    public class MotionSensor : MultyBoolStateDevice
    {
        public override string TypeName { get => "Датчик движения"; set => base.TypeName = value; }
        public override string Icon { get => "device_motionsensor.png"; set => base.Icon = value; }
        public override bool IsInput { get => true; set => base.IsInput = value; }

        public MotionSensor()
        {

        }

        public MotionSensor(string name, IEnumerable<bool> state): base(name, state)
        {
        }

        public override BaseEntity<UID> Clone()
        {
            return new MotionSensor() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
