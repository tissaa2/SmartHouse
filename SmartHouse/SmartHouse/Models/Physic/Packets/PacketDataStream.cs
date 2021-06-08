using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SmartHouse.Models.Packets
{
    public class PacketDataStream
    {
        public byte[] Data;
        private int readPosition = 0;
        private int writePosition = 0;

        public int ReadPosition
        {
            get => readPosition;
        }

        public int WritePosition
        {
            get => writePosition;
        }

        public bool Write(byte[] data)
        {
            if (writePosition + data.Length <= Data.Length)
            {
                Array.Copy(data, 0, Data, writePosition, data.Length);
                writePosition += data.Length;
                return true;
            }
            return false;
        }

        public bool Write(byte data)
        {
            return Write(new byte[] { data });
        }

        public bool Write(UID data)
        {
            return Write(new byte[] { data.B2, data.B1, data.B0 });
        }

        public byte[] Read(int count)
        {
            byte[] result = null;
            if (readPosition + count <= Data.Length)
            {
                result = new byte[count];
                Array.Copy(Data, readPosition, result, 0, count);
                readPosition += count;
            }
            else
            {
                result = null;
            }
            return result;
        }



        /// <summary>
        /// Reads to end of stream
        /// </summary>
        /// <returns></returns>
        public byte[] Read()
        {
            return Read(Data.Length - readPosition);
        }

        public byte[] ReadBytes(int count, ref int pos)
        {
            return Read(count);
        }

        public byte ReadByte()
        {
            byte[] array = Read(1);
            bool flag = array != null;
            if (flag)
            {
                return array[0];
            }
            throw new Exception("DuplexStream: error reading byte - timeout reached");
        }

        public short ReadInt16()
        {
            byte[] array = Read(2);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int16 - timeout reached");
        }

        public int ReadInt32()
        {
            byte[] array = Read(4);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int32 - timeout reached");
        }

        public long ReadInt64()
        {
            byte[] array = Read(8);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int64 - timeout reached");
        }

        public void Assign(byte[] data)
        {
            Data = data;
        }

        public PacketDataStream(byte[] data)
        {
            Assign(data);
        }
    }
}
