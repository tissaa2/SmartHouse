using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{

    [IconName("device_motionsensor.png", "Датчик движения", true)]
    public class MotionSensor : MultyBoolStateDevice
    {
        public override string TypeName { get => "Датчик движения"; set => base.TypeName = value; }
        public override string Icon { get => "device_motionsensor.png"; set => base.Icon = value; }
        public override bool IsInput { get => true; set => base.IsInput = value; }

        public MotionSensor()
        {

        }

        public MotionSensor(int id, string name, IEnumerable<bool> state, UID uid, byte portID) : base(id, name, state, uid, portID)
        {
            Init();
        }

        public override BaseEntity Clone()
        {
            return new MotionSensor() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
