using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Services
{
    public class Utils
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
    }
}

