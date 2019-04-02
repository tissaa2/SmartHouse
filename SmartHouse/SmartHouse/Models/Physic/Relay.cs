using SmartHouse.Models.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models.Physics
{
    [DeviceType(2)]
    public class Relay: PDevice
    {
        public override string Icon { get => "pdevice_relay.png"; }
        public override string TypeName { get => "Реле"; }

        /* public override void Init(int inputsCount, int outputsCount)
        {
            base.Init(inputsCount, outputsCount);
        } */

        //public override byte[] CreateWriteScenePacket(UID uid, byte sceneNumber, byte quadNum, byte intensity0, byte intensity1, byte intensity2, byte intensity3)
        //{
        //    return Packet.CreateRelaySceneIntensityWriteRequest(uid, sceneNumber, quadNum, intensity0, intensity1, intensity2, intensity3);
        //}

        public override byte[] CreateWriteScenePacket(UID uid, byte sceneNumber, byte offset, bool isNight, byte[] intensity)
        {
            return Packet.CreateRelaySceneSwitchesWriteRequest(uid, sceneNumber, isNight, intensity);
        }

        public Relay()
        {

        }

        /* public Relay(UID id, int inputs, int outputs)
        {
            Init(id, inputs, outputs, true);
        } */

        public override PDevice Assign(PDevice source)
        {
            return base.Assign(source);
        }

        public override PDevice Clone()
        {
            return (new Relay()).Assign(this);
        }

    }
}
