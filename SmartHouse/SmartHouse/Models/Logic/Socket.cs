using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    [IconName("device_socket.png", "Розетка")]
    public class Socket : StateDevice<bool>
    {
        public override string TypeName { get => "Розетка"; set => base.TypeName = value; }
        public override string Icon { get => "device_socket.png"; set => base.Icon = value; }

        public Socket()
        {

        }

        public Socket(string name, bool state): base(name, state)
        {
        }


        public override BaseEntity<UID> Clone()
        {
            return new Socket() { ID = ID, Icon = Icon, Name = Name, SecurityLevel = SecurityLevel, State = State };
        }

    }
}
