﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using SmartHouse.Models;

namespace SmartHouse.Services
{
    public class UDPConnection
    {
        public delegate void OnReceiveDataDelegate(UDPConnection sender, byte[] data);

        public DuplexStream Stream;

        public object Tag;

        public IPEndPoint LocalAddress;

        protected UdpClient socket = null;

        public event UDPConnection.OnReceiveDataDelegate OnReceiveData;

        protected virtual void ProcessData(IAsyncResult result, IPEndPoint remoteAddress, byte[] data)
        {
            Log.Write(string.Format("{0}: {1}", remoteAddress, Utils.BytesToHexString(data)));
        }

        private void OnUdpData(IAsyncResult result)
        {
            UdpClient udpClient = result.AsyncState as UdpClient;
            IPEndPoint remoteAddress = new IPEndPoint(0L, 0);
            byte[] array = udpClient.EndReceive(result, ref remoteAddress);
            this.ProcessData(result, remoteAddress, array);
            this.Stream.Write(array);
            this.OnReceiveData?.Invoke(this, array);
            udpClient.BeginReceive(new AsyncCallback(this.OnUdpData), udpClient);
        }

        public virtual void Setup(IPEndPoint localAddress)
        {
            this.LocalAddress = localAddress;
            this.socket = new UdpClient(localAddress);
            this.Stream = new DuplexStream(-1, 32768);
            this.socket.DontFragment = true;
        }

        public virtual void Start()
        {
            this.socket.BeginReceive(new AsyncCallback(this.OnUdpData), this.socket);
        }

        public UDPConnection(IPEndPoint localAddress)
        {
            this.Setup(localAddress);
        }

        public virtual void Send(string message, IPEndPoint remoteAddress)
        {
            this.Send(Encoding.UTF8.GetBytes(message), remoteAddress);
        }

        public virtual void Send(byte[] data, IPEndPoint remoteAddress)
        {
            int num = this.socket.Send(data, data.Length, remoteAddress);
        }
    }
}
