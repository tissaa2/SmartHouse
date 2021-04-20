using SmartHouse.Models.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    [DeviceType(1)]
    public class Dimmer: PDevice
    {
        public override string Icon { get => "pdevice_dimmer.png"; }
        public override string TypeName { get => "Диммер"; }

        public override byte[] CreateWriteScenePacket(UID uid, byte sceneNumber, byte offset, bool isNight, byte[] intensity)
        {
            return Packet.CreateDimmerSceneIntensityWriteRequest(uid, sceneNumber, isNight, offset > 0, intensity[offset], intensity[offset + 1], intensity[offset + 2], intensity[offset + 3]);
        }

        public Dimmer()
        {

        }

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new Dimmer()).Assign(this);
        }

    }
}
