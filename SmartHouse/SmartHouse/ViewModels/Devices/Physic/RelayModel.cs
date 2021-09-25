using SmartHouse.Models;
using SmartHouse.Models.Packets;

namespace SmartHouse.ViewModels.Devices.Physic
{
    [DeviceType(2)]
    public class RelayModel: PhysicDeviceModel
    {
        public override string Icon { get => "pdevice_relay.png"; }
        public override string TypeName { get => "Реле"; }


        public override byte[] CreateWriteScenePacket(UID uid, byte sceneNumber, byte offset, bool isNight, byte[] intensity)
        {
            return Packet.CreateRelaySceneSwitchesWriteRequest(uid, sceneNumber, isNight, intensity);
        }

        public override ViewModel Clone()
        {
            var m = new MSTPanelModel();
            m.Assign(this);
            return m;
        }

    }
}
