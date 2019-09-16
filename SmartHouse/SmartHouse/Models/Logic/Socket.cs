using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    [IconName("device_socket.png", "Розетка", false)]
    public class Socket : BoolStateDevice
    {
        public override string TypeName { get => "Розетка"; set => base.TypeName = value; }
        public override string Icon { get => "device_socket.png"; set => base.Icon = value; }

        public Socket()
        {

        }

        public Socket(int id, string name, bool state, UID uid, byte portID) : base(id, name, state, uid, portID)
        {
        }


        public override BaseEntity<int> Clone()
        {
            return new Socket() { ID = ID, UID = UID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
