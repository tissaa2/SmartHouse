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

        /* public override void Init(int inputsCount, int outputsCount)
        {
            base.Init(inputsCount, outputsCount);
        } */

        //public override byte[] CreateWriteScenePacket(UID uid, byte sceneNumber, byte quadNum, byte intensity0, byte intensity1, byte intensity2, byte intensity3)
        //{
        //    return Packet.CreateDimmerSceneIntensityWriteRequest(uid, sceneNumber, intensity0, intensity1, intensity2, intensity3);
        //}

        public override byte[] CreateWriteScenePacket(UID uid, byte sceneNumber, byte offset, bool isNight, byte[] intensity)
        {
            return Packet.CreateDimmerSceneIntensityWriteRequest(uid, sceneNumber, isNight, offset > 0, intensity[offset], intensity[offset + 1], intensity[offset + 2], intensity[offset + 3]);
        }

        public Dimmer()
        {

        }

        /* public Dimmer(UID id, int inputs, int outputs)
        {
            Init(id, inputs, outputs, true);
        } */

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
