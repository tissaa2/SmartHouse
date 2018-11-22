using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models
{
    public class Settings: BaseObject
    {
        public static string FileName { get; set; } = "settings.json";
        private static Settings instance = null;
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Load<Settings>(FileName);
                    if (instance == null)
                    {
                        instance = new Settings();
                        instance.Save();
                    }
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public void Save()
        {
            Save(FileName);
        }

        public Boolean IsAdmin { get; set; } = true;
        public string IP { get; set; } = "0.0.0.0";
        public int Port { get; set; } = 61576;
    }
}
