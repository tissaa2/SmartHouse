using SmartHouse.Helpers;
using SmartHouse.Models;
using SmartHouse.Models.Packets;
using SmartHouse.Models.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Services
{
    public class CAN
    {
        public static CAN Instance
        {
            get;
            set;
        }
        public void SetPortValue(UID deviceId, int portId, byte value, byte fadeTime)
        {
            // return;
            if (Client.CurrentServer == null)
                return;
            if (deviceId.Hash == 0)
                return;
            var data = Packet.CreateSetOutputValueRequest(deviceId, (byte)portId, value, 0, fadeTime, 0, 0);
            Client.CurrentServer.SendToCAN(data, 20000, Packet.GetControllerCommand(data), Packet.GetCANCommand(data), deviceId, (p, e) => { return e; }, (p, e) => { return e; });
        }

        public async Task<bool> WriteScenes(UID deviceID, IDictionary<int, Scene> items)
        {
            int i = 0;
            if (await Utils.P(Packet.CreateDeviceFlashRequest(deviceID, 0, 0, 1), deviceID))
            {
                if (await Utils.P(Packet.CreateDevicePortSettingsWriteRequest(deviceID, new byte[] { 5, 5, 5, 5, 5, 5, 5, 5 }), deviceID))
                {
                    foreach (var ps in items.Values)
                    {
                        if (await Utils.P(Packet.CreateSceneWriteRequest(deviceID, (byte)i, 0, (byte)(i == 0 ? 1 : 0)), deviceID))
                        {
                            // сделать анализ ответов от диммера 
                            if (await Utils.P(Packet.CreateSceneEventSettingsWriteRequest(deviceID, (byte)i, ps.SourceID, ps.SourcePort), deviceID))
                            {
                                if (await Utils.P(Packet.CreateSceneParamsSettingsWriteRequest(deviceID, (byte)i, 0x80, 5), deviceID))
                                {
                                    byte[] stts = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                                    foreach (var st in ps.OutputStates)
                                        stts[st.Key] = (byte)st.Value;
                                    if (this is Dimmer)
                                    {
                                        // byte[] stts = ps.OutputStates.Values.Select(e => (byte)e).ToArray();
                                        if (!await Utils.P(CreateWriteScenePacket(ID, (byte)i, 0, false, stts), ID))
                                            Log.Write("Error writing scene intensity quad: device = {0}({1}), sceneNum = {2}, offset = 0", this.Name, this.ID, i);
                                        if (!await Utils.P(CreateWriteScenePacket(ID, (byte)i, 4, false, stts), ID))
                                            Log.Write("Error writing scene intensity quad: device = {0}({1}), sceneNum = {2}, offset = 4", this.Name, this.ID, i);
                                        if (!await Utils.P(CreateWriteScenePacket(ID, (byte)i, 0, true, stts), ID))
                                            Log.Write("Error writing scene intensity quad: device = {0}({1}), sceneNum = {2}, offset = 0", this.Name, this.ID, i);
                                        if (!await Utils.P(CreateWriteScenePacket(ID, (byte)i, 4, true, stts), ID))
                                            Log.Write("Error writing scene intensity quad: device = {0}({1}), sceneNum = {2}, offset = 4", this.Name, this.ID, i);
                                    }
                                    else
                                    if (this is Relay)
                                    {
                                        //byte[] stts = ps.OutputStates.Values.Select(e => (byte)e).ToArray();
                                        if (!await Utils.P(CreateWriteScenePacket(ID, (byte)i, 0, false, stts), ID))
                                            Log.Write("Error writing scene intensity quad: device = {0}({1}), sceneNum = {2}", this.Name, this.ID, i);
                                        if (!await Utils.P(CreateWriteScenePacket(ID, (byte)i, 4, true, stts), ID))
                                            Log.Write("Error writing scene intensity quad: device = {0}({1}), sceneNum = {2}", this.Name, this.ID, i);
                                    }
                                }
                            }
                            else
                            {
                                Log.Write("Error starting scene writing: device = {0}({1}), sceneNum = {2}", this.Name, this.ID, i);
                                return false;
                            }
                        } // продублировать сохранение для ночной сцены 
                        else
                        {
                            Log.Write("Error starting scene writing: device = {0}({1}), sceneNum = {2}", this.Name, this.ID, i);
                            return false;
                        }
                        if (!await Utils.P(Packet.CreateSceneWriteRequest(ID, (byte)i, 1, 0), ID))
                        {
                            Log.Write("Error finishing scene writing: device = {0}({1}), sceneNum = {2}", this.Name, this.ID);
                            return false;
                        }
                        i++;
                    }
                    if (!await Utils.P(Packet.CreateDeviceFlashRequest(ID, 0, 1, 1), ID))
                    {
                        Log.Write("Error finishing device flashing: device = {0}({1})", this.Name, this.ID);
                        return false;
                    }
                }
                else
                    Log.Write("Error writing device port settings: device = {0}({1})", this.Name, this.ID);
            }
            else
            {
                Log.Write("Error starting device flashing: device = {0}({1})", this.Name, this.ID);
                return false;
            }
            return true;
        }


    }
}
