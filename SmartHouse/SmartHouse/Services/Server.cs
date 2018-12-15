using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SmartHouse.Models;
using SmartHouse.Models.Packets;
using System.Threading;
using SmartHouse.Models.Packets.Processors;
using System.Linq;
using System.Threading.Tasks;
using SmartHouse.Models.Logic;

namespace SmartHouse.Services
{
    public class Server : UDPConnection
    {
        public List<ActionCallback> PacketCallbacks { get; set; } = new List<ActionCallback>();
        // public List<ActionCallback> CANPacketCallbacks { get; set; } = new List<ActionCallback>();

        public Thread MainThread = new Thread(MainThreadRun);

        public ServerFile CurrentFile { get; set; } = null;

        public static void MainThreadRun(object obj)
        {
            var server = obj as Server;
            while (true)
            {
                Packet p = Packet.Read(server.Stream);
                object d = p;
                if (p.DataSize >= 0)
                {
                    if (PacketProcessor.Processors.ContainsKey(p.Command))
                    {
                        d = PacketProcessor.Processors[p.Command].ProcessPacket(p);
                    }
                }
                lock (server.PacketCallbacks)
                {
                    var c = server.PacketCallbacks.FirstOrDefault(e => e.Data == p.Command);
                    if (c != null)
                    {
                        server.PacketCallbacks.Remove(c);
                        c.Callback?.Invoke(d);
                    }
                }
            }
        }


        public void AddPacketCallback(ActionCallback callback)
        {
            lock (PacketCallbacks)
            {
                PacketCallbacks.Add(callback);
            }
        }

        /* public void AddCANPacketCallback(ActionCallback callback)
        {
            lock (CANPacketCallbacks)
            {
                CANPacketCallbacks.Add(callback);
            }
        } */

        public UID ID;
        public IPEndPoint RemoteAddress;

        public Server(IPEndPoint localAddress, IPEndPoint remoteAddress) : base(localAddress)
        {
            this.RemoteAddress = remoteAddress;
        }

        public virtual void Send(byte[] data)
        {
            this.socket.Send(data, data.Length, this.RemoteAddress);
        }

        public void Send(byte[] command, int timeout, int packetType, ActionCallbackDelegate onSuccess)
        {
            AddPacketCallback(new ActionCallback() { Callback = onSuccess, Data = packetType, ExpireTime = timeout * 10000 + DateTime.Now.Ticks });
            Send(command);
        }

        public void SendAndWaitForResponse(byte[] command, Int16 packetType, string message, ProcessPacketDelegate onSuccess)
        {
            Send(command, 20000, packetType, (o) => {
                if (o is Packet)
                {
                    var p = o as Packet;
                    if (p.DataSize >= 0)
                    {
                        CommandConfirmation cc = CommandConfirmation.Read(p.Data);
                        Log.Write("Command {0} executed: result = {1}", message, cc.Result);
                        onSuccess?.Invoke(p);
                    }
                    else
                        Log.Write("Command {0} sent: result did not arrived", message);
                }
            });
        }

        public async Task<CommandConfirmation> SendAndWaitForConfirm(byte[] command, Int16 packetType, string message)
        {
            var res = await Task.Run(() =>
            {
                CommandConfirmation cc = null;
                while (!Client.Instance.Initialized)
                    Thread.Sleep(100);
                Send(command, 20000, packetType, (o) => {
                    if (o is Packet)
                    {
                        var p = o as Packet;
                        if (p.DataSize >= 0)
                        {
                            cc = CommandConfirmation.Read(p.Data);
                            Log.Write("Command {0} executed: result = {1}", message, cc.Result);
                        }
                        else
                            Log.Write("Command {0} sent: result did not arrived", message);
                    }
                });
                while (cc == null)
                    Thread.Sleep(1);
                return cc;
            });
            return res;
        }

        public async Task<Packet> SendAndWaitForResponse(byte[] command, Int16 packetType, string message)
        {
            var res = await Task.Run(() =>
            {
                Packet p = null;
                while (!Client.Instance.Initialized)
                    Thread.Sleep(100);
                Send(command, 20000, packetType, (o) => {
                    if (o is Packet)
                    {
                        p = o as Packet;
                        if (p.DataSize >= 0)
                        {
                            Log.Write("Command {0} executed: result = {1}", message, p);
                        }
                        else
                            Log.Write("Command {0} sent: result did not arrived", message);
                    }
                });
                while (p == null)
                    Thread.Sleep(1);
                return p;
            });
            return res;
        }

        public override void Start()
        {
            base.Start();
            MainThread.Start(this);
        }

        //private byte CalcCRC(byte[] data, int offset, int size)
        //{
        //    byte res = 0;
        //    var bts = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(offset));
        //    for (int i = 0; i < bts.Length; i++)
        //        res = (byte)(res + bts[i]);
        //    for (int i = offset; i < offset + size; i++)
        //        res = (byte)(res + data[i]);
        //    return (byte)((0x100 - res) & 0xFF);
        //}

        private byte CalcCRC(byte[] data, int dataOffset, int offset, int size)
        {
            byte res = 0;
            var bts = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(offset));
            for (int i = 0; i < bts.Length; i++)
                res = (byte)(res + bts[i]);
            // res = (res + bts[i]) & 0xFF;
            for (int i = dataOffset; i < dataOffset + size; i++)
                res = (byte)(res + data[i]);
            // res = (res + data[i]) & 0xFF;
            return (byte)((0x100 - res) & 0xFF);
        }

        // 0: Сохранить проект в CAN
        public async void SaveProjectFile(byte[] data)
        {
            CommandConfirmation res = await Client.CurrentServer.SendAndWaitForConfirm(Packet.WriteAliasFileRequest, Packet.WRITE_ALIASFILE_COMMAND, "open alias file for writing");
            if (res.Result == 0)
            {
                int i = 0;
                byte crc = 0;
                int packetDataSize = 64;
                while (i < data.Length)
                {
                    var size = (i + packetDataSize > data.Length) ? data.Length - i : packetDataSize;
                    byte[] packet;
                    // do
                    {
                        byte pds = (byte)(4 /*offset*/ + size + 1/*crc*/);
                        packet = new byte[Packet.WriteFileChunkRequest.Length + pds];
                        Packet.WriteFileChunkRequest.CopyTo(packet, 0);
                        packet[5] = pds;
                        var bts = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
                        packet[6] = bts[0];
                        packet[7] = bts[1];
                        packet[8] = bts[2];
                        packet[9] = bts[3];
                        crc = CalcCRC(data, i, i, size);
                        Array.Copy(data, i, packet, 10, size);
                        packet[10 + size] = crc;

                        res = await Client.CurrentServer.SendAndWaitForConfirm(packet, Packet.WRITE_FILECHUNK_COMMAND, "write file chunk");
                    }
                    // while (res.Result != 0);
                    if (res.Result != 0)
                    {
                        Log.Write("Error writing file chunk, pos = {0}, size = {1}, crc = {2}, data = ({3})", i, size, crc, BitConverter.ToString(packet, 10, size).Replace("-", ","));
                        break;
                    }
                    i += size;
                }

                do
                {
                    res = await Client.CurrentServer.SendAndWaitForConfirm(Packet.WriteFileEndRequest, Packet.FINISH_WRITE_ALIASFILE_COMMAND, "alias file writing complete");
                }
                while (res.Result != 0);
            }
        }

        public async Task<bool> SaveProjectToCAN(Project project)
        {
            foreach(var g in project.Items)
                foreach(var s in g.Items)
                {
                    // пишем сцену в CAN await 
                    Packet res = await Client.CurrentServer.SendAndWaitForResponse(Packet.ReadAliasFileRequest, Packet.READ_ALIASFILE_COMMAND, "open alias file for reading");
                }
            return true;
        }

        public async Task<ServerFile> LoadProjectFile()
        {
            Packet res = await Client.CurrentServer.SendAndWaitForResponse(Packet.ReadAliasFileRequest, Packet.READ_ALIASFILE_COMMAND, "open alias file for reading");
            if (res.Command == Packet.READ_ALIASFILE_COMMAND && res.DataSize > 1)
            {
                int i = 0;
                int packetDataSize = 64;
                FileHeaderData fh = FileHeaderData.Read(res.Data);
                ServerFile f = new ServerFile(fh.Size);
                while (i < fh.Size)
                {
                    var size = (i + packetDataSize > fh.Size) ? fh.Size - i : packetDataSize;
                    // do
                    {
                        byte[] packet = Packet.ReadFileChunkRequest;
                        var bts = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
                        packet[6] = bts[0];
                        packet[7] = bts[1];
                        packet[8] = bts[2];
                        packet[9] = bts[3];
                        res = await Client.CurrentServer.SendAndWaitForResponse(packet, Packet.READ_FILECHUNK_COMMAND, "read file chunk");
                    }
                    // while (res.DataSize < 2);
                    if (res.DataSize < 2)
                    {
                        Log.Write("Error reading file chunk, pos = {0}", i);
                        break;
                    }
                    FileChunkData fc = FileChunkData.Read(res.Data);
                    var crc = CalcCRC(fc.Data, 0, fc.Offset, fc.Data.Length);
                    if (crc == fc.CRC)
                    {
                        f.Write(fc.Offset, fc.Data);
                    }
                    else
                    {
                        Log.Write("Error loading file. CRC doesn't match");
                    }
                    i += size;
                }

                CommandConfirmation cc = null;
                do
                {
                    cc = await Client.CurrentServer.SendAndWaitForConfirm(Packet.FinishAliasFileReadRequest, Packet.FINISH_READ_ALIASFILE_COMMAND, "alias file read complete");
                }
                while (cc.Result != 0);

                f.Complete = true;
                return f;
            }
            return null;
        }
    }
}
