using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;

namespace SmartHouse.Services
{
    public class Client : UDPConnection
    {
        public int BroadcastPort = 0;

        public static Server CurrentServer = null;

        public Dictionary<IPEndPoint, Server> ServersList = new Dictionary<IPEndPoint, Server>();

        protected Thread MainThread;

        protected byte previousSceneId = 0;

        public Client(IPEndPoint localAddress) : base(localAddress)
        {
            this.MainThread = new Thread(new ParameterizedThreadStart(this.MainThreadRun));
        }

        protected CommandResponsePacket SendRequestAndWaitForResponse(Server server, byte[] command, string message)
        {
            CommandResponsePacket commandResponsePacket = new CommandResponsePacket();
            server.Send(command);
            int num = commandResponsePacket.Read(server.Stream);
            bool flag = num >= 0;
            CommandResponsePacket result;
            if (flag)
            {
                Log.Write("Command {0} executed: result = {1}", new object[]
                {
                    message,
                    (commandResponsePacket.Result == 0) ? "success" : "failure"
                });
                result = commandResponsePacket;
            }
            else
            {
                Log.Write("Command {0} sent: result did not arrived", new object[]
                {
                    message
                });
                result = null;
            }
            return result;
        }

        private byte GetPortFromMask(byte mask)
        {
            byte result;
            for (byte b = 0; b < 8; b += 1)
            {
                bool flag = ((int)mask & 1 << (int)b) == 0;
                if (flag)
                {
                    result = b;
                    return result;
                }
            }
            result = 0;
            return result;
        }

        protected void MainThreadRun(object arg)
        {
            this.Broadcast(Packet.DiscoverRequest, this.BroadcastPort);
            Thread.Sleep(10);
            Packet packet = new Packet();
            CommandResponsePacket commandResponsePacket = new CommandResponsePacket();
            packet.ReadHeader(this.Stream);
            packet = Packet.DeriveAndLoadData(packet, this.Stream);
            bool flag = packet is DiscoverResponsePacket;
            if (flag)
            {
                DiscoverResponsePacket discoverResponsePacket = packet as DiscoverResponsePacket;
                Log.Write("Got discover response: {0}", new object[]
                {
                    packet
                });
                Log.Write("Sending port select request..");
                byte portFromMask = this.GetPortFromMask(discoverResponsePacket.PortMask);
                Packet.PortSelectRequest[6] = portFromMask;
                this.Broadcast(Packet.PortSelectRequest, this.BroadcastPort);
                commandResponsePacket.Read(this.Stream);
                Log.Write("Got port select response: {0}", new object[]
                {
                    commandResponsePacket
                });
                Client.CurrentServer.RemoteAddress.set_Port(61576 + (int)portFromMask);
                this.SendRequestAndWaitForResponse(Client.CurrentServer, Packet.CheckConnectionRequest, "check connection");
                this.SendRequestAndWaitForResponse(Client.CurrentServer, Packet.InitCANTranslationRequest, "init CAN");
                Thread.Sleep(1000);
            }
        }

        public void DoTestSequence()
        {
            Packet.ActivateSceneCANRequest[11] = 0;
            Packet.ActivateSceneCANRequest[15] = 2;
            this.SendRequestAndWaitForResponse(Client.CurrentServer, Packet.ActivateSceneCANRequest, "activate scene 0");
            Thread.Sleep(7000);
            Packet.ActivateSceneCANRequest[11] = 1;
            Packet.ActivateSceneCANRequest[15] = 0;
            this.SendRequestAndWaitForResponse(Client.CurrentServer, Packet.ActivateSceneCANRequest, "activate scene 1");
            Thread.Sleep(7000);
            Packet.ActivateSceneCANRequest[11] = 2;
            Packet.ActivateSceneCANRequest[15] = 1;
            this.SendRequestAndWaitForResponse(Client.CurrentServer, Packet.ActivateSceneCANRequest, "activate scene 2");
        }

        public void ActivateScene(byte id)
        {
            Packet.ActivateSceneCANRequest[11] = id;
            Packet.ActivateSceneCANRequest[15] = this.previousSceneId;
            this.previousSceneId = id;
            this.SendRequestAndWaitForResponse(Client.CurrentServer, Packet.ActivateSceneCANRequest, string.Format("activate scene {0}", id));
            Thread.Sleep(1000);
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
            this.BroadcastPort = broadcastPort;
            this.MainThread.Start();
        }
    }
}
