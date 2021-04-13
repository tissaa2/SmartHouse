using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Services
{
    public class LogEntry
    {
        public string Time
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public LogEntry(string time, string text)
        {
            this.Time = time;
            this.Text = text;
        }
    }
}
