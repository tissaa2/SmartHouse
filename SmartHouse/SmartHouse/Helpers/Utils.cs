using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using SmartHouse.Models.Packets;

namespace SmartHouse.Services
{
    public static class Utils
    {
        public static bool EmulateCAN { get; set; } = false;
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

        public static List<Type> FindAllDerivedTypes<T>()
        {
            return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T)));
        }

        public static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            Type derivedType = typeof(T);
            return Enumerable.ToList<Type>(Enumerable.Where<Type>(assembly.GetTypes(), (Type t) => t != derivedType && derivedType.IsAssignableFrom(t)));
        }

        public static async Task<bool> P(byte[] request)
        {
            var p = await Client.CurrentServer.SendAndWaitForResponse(request, Packet.GetCANCommand(request), "");
            var cc = CommandConfirmation.Read(p.Data);
            return cc.Result == 0;
        }

    }
}

