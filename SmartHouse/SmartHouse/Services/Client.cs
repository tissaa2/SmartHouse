using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using SmartHouse.Models.Packets;
using SmartHouse.Models;

namespace SmartHouse.Services
{

    public delegate void ProcessPacketDelegate(Packet packet);
    public class Client : UDPConnection
    {
        private static Client instance = null;
        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client(new IPEndPoint(IPAddress.Any, 0));
                    // instance.Start(61576);
                    instance.Start(SmartHouse.Models.Settings.Instance.Port);
                }
                return instance;
            }
        }

        public int BroadcastPort = 0;

        public static Server CurrentServer = null;

        public Dictionary<IPEndPoint, Server> ServersList = new Dictionary<IPEndPoint, Server>();

        public bool Initialized { get; private set; } = false;

        protected Thread MainThread;

        protected byte previousSceneId = 0;

        public Client(IPEndPoint localAddress) : base(localAddress)
        {
            this.MainThread = new Thread(new ParameterizedThreadStart(this.MainThreadRun));
        }

        /* public void SendRequestAndWaitForResponse(Server server, byte[] command, string message, ProcessPacketDelegate onResponse)
        {
            server.Send(command);
            Packet p = Packet.Read(server.Stream);
            bool flag = p.DataSize >= 0;
            int i = 0;
            if (flag)
            {
                Log.Write("Command {0} executed: result = success", message);
            }
            else
            {
                Log.Write("Command {0} sent: result did not arrived", message);
            }
            onResponse?.Invoke(p);
        }

        public void SendAndProcessResponses(Server server, byte[] command, int timeout, string message, ProcessPacketDelegate onPacket)
        {
            server.Send(command);
            long t = timeout * 10000 + DateTime.Now.Ticks;
            while (t > DateTime.Now.Ticks)
            // while (true)
                {
                    // ControllerCommandResponsePacket commandResponsePacket = new ControllerCommandResponsePacket();
                    // int num = commandResponsePacket.Read(server.Stream);
                    Packet p = Packet.Read(server.Stream);
                bool flag = p.DataSize >= 0;
                if (flag)
                {
                    onPacket?.Invoke(p);
                }
            }
        } */

        private byte GetPortFromMask(byte mask)
        {
            byte result;
            for (byte b = 0; b < 8; b += 1)
            {
                bool flag = ((int)mask & 1 << (int)b) == 0;
                if (flag)
                {
                    result = b;
                    result++;
                    return result;
                }
            }
            result = 0;
            return result;
        }

        protected void MainThreadRun(object arg)
        {
            Initialized = false;
            this.Broadcast(Packet.DiscoverRequest, this.BroadcastPort);

            Thread.Sleep(10);
            Packet packet = Packet.Read(Stream);
            ControllerDiscoverPacketData discoverResponse = ControllerDiscoverPacketData.Read(packet.Data);
            Log.Write("Got discover response: {0}", packet);
            Log.Write("Sending port select request..");
            byte controllerPort;
            if (discoverResponse.PortNumber != 0)
            {
                controllerPort = discoverResponse.PortNumber;
                Log.Write("Port {0} is already open for you", discoverResponse.PortNumber);
            }
            else
            {
                controllerPort = this.GetPortFromMask(discoverResponse.PortMask);
                Packet.PortSelectRequest[6] = controllerPort;
                this.Broadcast(Packet.PortSelectRequest, this.BroadcastPort);
                packet = Packet.Read(this.Stream);
                CommandConfirmation cc = CommandConfirmation.Read(packet.Data);
                Log.Write("Got port select response: {0}", cc);
            }
            Client.CurrentServer.RemoteAddress.Port = Settings.Instance.Port + (int)controllerPort;
            // this.SendRequestAndWaitForResponse(Client.CurrentServer, Packet.CheckConnectionRequest, "check connection");
            Client.CurrentServer.SendAndWaitForResponse(Packet.CheckConnectionRequest, 18, "check connection", (o0) => {
                Client.CurrentServer.SendAndWaitForResponse(Packet.InitCANTranslationRequest, 32, "init CAN", (o1) =>
                {
                    Thread.Sleep(1000);
                    Initialized = true;
                });
            });
        }

        public void ActivateScene(byte id)
        {
            Packet.ActivateSceneCANRequest[11] = id;
            Packet.ActivateSceneCANRequest[15] = this.previousSceneId;
            this.previousSceneId = id;
            Client.CurrentServer.SendAndWaitForResponse(Packet.ActivateSceneCANRequest, 0x30, string.Format("activate scene {0}", id), null);
        }

        public void Broadcast(byte[] data, int port)
        {
            IPEndPoint remoteAddress = new IPEndPoint(IPAddress.Broadcast, port);
            this.Send(data, remoteAddress);
        }

        public void Broadcast(string data, int port)
        {
            IPEndPoint remoteAddress = new IPEndPoint(IPAddress.Broadcast, port);
            this.Send(data, remoteAddress);
        }

        protected override void ProcessData(IAsyncResult result, IPEndPoint remoteAddress, byte[] data)
        {
            bool flag = !this.ServersList.ContainsKey(remoteAddress);
            if (flag)
            {
                Client.CurrentServer = new Server(new IPEndPoint(IPAddress.Any, 0), remoteAddress);
                this.ServersList.Add(remoteAddress, Client.CurrentServer);
                Client.CurrentServer.Start();
                Log.Write(string.Format("Server found: {0}", remoteAddress));
            }
            base.ProcessData(result, remoteAddress, data);
        }

        public virtual void Start(int broadcastPort)
        {
            base.Start();
            if (broadcastPort > UInt16.MaxValue)
                broadcastPort = 61576;
            this.BroadcastPort = broadcastPort;
            this.MainThread.Start();
        }
    }
}
