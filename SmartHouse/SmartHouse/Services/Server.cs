using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace SmartHouse.Services
{
    public class Server : UDPConnection
    {
        public IPEndPoint RemoteAddress;

        public Server(IPEndPoint localAddress, IPEndPoint remoteAddress) : base(localAddress)
        {
            this.RemoteAddress = remoteAddress;
        }

        public virtual void Send(byte[] data)
        {
            int num = this.socket.Send(data, data.Length, this.RemoteAddress);
        }
    }
}
