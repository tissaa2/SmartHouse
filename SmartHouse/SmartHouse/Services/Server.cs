using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SmartHouse.Models;
using SmartHouse.Models.Packets;
using System.Threading;
using SmartHouse.Models.Packets.Processors;
using System.Linq;

namespace SmartHouse.Services
{
    public class Server : UDPConnection
    {
        public List<ActionCallback> PacketCallbacks { get; set; } = new List<ActionCallback>();
        // public List<ActionCallback> CANPacketCallbacks { get; set; } = new List<ActionCallback>();

        public Thread MainThread = new Thread(MainThreadRun);

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
                        Log.Write("Command {0} executed: result = {1}", message, (cc.Result == 0) ? "success" : "failure");
                        onSuccess?.Invoke(p);
                    }
                    else
                        Log.Write("Command {0} sent: result did not arrived", message);
                }
            });
        }

        public override void Start()
        {
            base.Start();
            MainThread.Start(this);
        }
    }
}
