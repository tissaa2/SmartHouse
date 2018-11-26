using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace SmartHouse.Services
{
    public static class Utils
    {

        private static long lastTapTime = 0;

        public static bool IsDoubleTap()
        {
            var t = DateTime.Now.Ticks;
            bool r = t - lastTapTime < 10000 * 500;
            lastTapTime = t;
            return r;
        }

        public static byte[] HexStringToBytes(string text)
        {
            string h = text.Replace(" ", "");
            return Enumerable.Range(0, h.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(h.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string BytesToHexString(byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                bool flag = stringBuilder.Length > 0;
                if (flag)
                {
                    stringBuilder.Append(' ');
                }
                bool flag2 = b < 32 || b > 126;
                if (flag2)
                {
                    stringBuilder.Append(string.Format("{0:x2}", b));
                }
                else
                {
                    stringBuilder.Append(string.Format("'{0}'", (char)b));
                }
            }
            return stringBuilder.ToString();
        }


        public static byte[] Read(byte[] buf, int count, ref int pos)
        {
            byte[] result = null;
            if (pos + count <= buf.Length)
            {
                result = new byte[count];
                Array.Copy(buf, pos, result, 0, count);
                pos += count;
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static byte[] ReadBytes(this byte[] buf, int count, ref int pos)
        {
            return Read(buf, count, ref pos);
        }

        public static byte ReadByte(this byte[] buf, ref int pos)
        {
            byte[] array = Read(buf, 1, ref pos);
            bool flag = array != null;
            if (flag)
            {
                return array[0];
            }
            throw new Exception("DuplexStream: error reading byte - timeout reached");
        }

        public static short ReadInt16(this byte[] buf, ref int pos)
        {
            byte[] array = Read(buf, 2, ref pos);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int16 - timeout reached");
        }

        public static int ReadInt32(this byte[] buf, ref int pos)
        {
            byte[] array = Read(buf, 4, ref pos);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int32 - timeout reached");
        }

        public static long ReadInt64(this byte[] buf, ref int pos)
        {
            byte[] array = Read(buf, 8, ref pos);
            bool flag = array != null;
            if (flag)
            {
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(array, 0));
            }
            throw new Exception("DuplexStream: error reading Int64 - timeout reached");
        }

    }
}

