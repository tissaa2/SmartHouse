using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;


namespace SmartHouse.ViewModels
{

    public class SettingsModel: ViewModel 
    {
        private bool isAdmin;
        public new bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; OnPropertyChanged("IsAdmin"); }
        }

        private string ip;
        public string IP
        {
            get { return ip; }
            set { ip = value; OnPropertyChanged("IP"); }
        }

        private int port;
        public int Port
        {
            get { return port; }
            set { port = value; OnPropertyChanged("Port"); }
        }


        public SettingsModel()
        {
        }

        public SettingsModel(Settings source)
        {
            Assign(source);
        }

        public void Apply(Settings target)
        {
            target.IsAdmin = IsAdmin;
            target.IP = IP;
            target.Port = Port;
            target.Save();
        }

        public void Assign(Settings source)
        {
            IP = source.IP;
            IsAdmin = source.IsAdmin;
            Port = source.Port;
        }
    }
}
