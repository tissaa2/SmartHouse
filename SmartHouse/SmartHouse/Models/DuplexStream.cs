using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models
{
    public class DuplexStream
    {
        private volatile int BufferSize = 65536;

        public volatile int ReadPosition = 0;

        public volatile int WritePosition = 0;

        public volatile byte[] Buffer;

        public volatile int Timeout = -1;

        public int Available
        {
            get
            {
                bool flag = this.ReadPosition <= this.WritePosition;
                int result;
                if (flag)
                {
                    result = this.WritePosition - this.ReadPosition;
                }
                else
                {
                    result = this.WritePosition + this.BufferSize - this.ReadPosition;
                }
                return result;
            }
        }

        public int WaitForAvailable(int count, int timeout)
        {
            long num = DateTime.Now.Ticks + (long)(timeout * 10000);
            int result;
            while (DateTime.Now.Ticks < num || timeout == -1)
            {
                bool flag = this.Available >= count;
                if (flag)
                {
                    result = 1;
                    return result;
                }
            }
            result = 0;
            return result;
        }

        public byte ReadByte()
        {
            byte[] array = this.ReadBytes(1);
            bool flag = array != null;
            if (flag)
            {
                return array[0];
            }
            throw new Exception("DuplexStream: error reading byte - timeout reached");
        }

        public short ReadInt16()
        {
            byte[] array = this.ReadBytes(2);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int16 - timeout reached");
        }

        public int ReadInt32()
        {
            byte[] array = this.ReadBytes(4);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int32 - timeout reached");
        }

        public long ReadInt64()
        {
            byte[] array = this.ReadBytes(8);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int64 - timeout reached");
        }

        public DuplexStream(int timeout, int bufferSize = 32768)
        {
            this.BufferSize = bufferSize;
            this.Buffer = new byte[this.BufferSize];
            this.Timeout = timeout;
        }

        public int Write(byte[] bytes)
        {
            byte[] buffer = this.Buffer;
            lock (buffer)
            {
                bool flag2 = bytes.Length + this.WritePosition < this.BufferSize;
                if (flag2)
                {
                    bytes.CopyTo(this.Buffer, this.WritePosition);
                    this.WritePosition += bytes.Length;
                }
                else
                {
                    int num = this.BufferSize - this.WritePosition;
                    Array.Copy(bytes, 0, this.Buffer, this.WritePosition, num);
                    Array.Copy(bytes, 0, this.Buffer, 0, bytes.Length - num);
                    this.WritePosition = bytes.Length - num;
                }
            }
            return this.WritePosition;
        }

        public byte[] ReadBytes(int count)
        {
            bool flag = this.WaitForAvailable(count, this.Timeout) > 0;
            byte[] result;
            if (flag)
            {
                byte[] array = new byte[count];
                byte[] buffer = this.Buffer;
                lock (buffer)
                {
                    bool flag3 = this.ReadPosition + count <= this.BufferSize;
                    if (flag3)
                    {
                        Array.Copy(this.Buffer, this.ReadPosition, array, 0, count);
                        this.ReadPosition += count;
                    }
                    else
                    {
                        int num = this.BufferSize - this.ReadPosition;
                        Array.Copy(this.Buffer, this.ReadPosition, array, 0, num);
                        Array.Copy(this.Buffer, 0, array, num, count - num);
                        this.ReadPosition = count - num;
                    }
                }
                result = array;
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
