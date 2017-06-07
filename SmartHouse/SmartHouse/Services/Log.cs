using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse;

namespace SmartHouse.Services
{
    public class Log
    {
        public static void Write(string text)
        {
            MainPage.Instance.AddToLog(new LogEntry(DateTime.get_Now().ToString("dd.MM.yyyy hh:mm:ss"), text));
        }

        public static void Write(string template, params object[] items)
        {
            Log.Write(string.Format(template, items));
        }

        public static void Write(Exception ex)
        {
            Log.Write("{0} {1} {2}", new object[]
            {
                ex.get_Message(),
                ex.get_StackTrace(),
                ex.get_Source()
            });
        }
    }
}
