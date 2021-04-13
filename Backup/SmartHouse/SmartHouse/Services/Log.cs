using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Views;

namespace SmartHouse.Services
{
    public class Log
    {
        private static Log instance = null;
        public static Log Instance
        {
            get
            {
                if (instance == null)
                    instance = new Log();
                return instance;
            }
        }

        public static void Write(string text)
        {
            // if (DebugPage.Instance == null)
            //    return;
            
            // DebugPage.Instance.AddToLog(new LogEntry(DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"), text));

            Instance.Entries.Insert(0, new LogEntry(DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"), text));
        }

        public static void Write(string template, params object[] items)
        {
            Log.Write(string.Format(template, items));
        }

        public static void Write(Exception ex)
        {
            Log.Write("{0} {1} {2}", new object[]
            {
                ex.Message,
                ex.StackTrace,
                ex.Source
            });
        }

        public ObservableCollection<LogEntry> Entries { get; set; } = new ObservableCollection<LogEntry>();
    }
}
