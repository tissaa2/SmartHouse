using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class DevicePageModel: INotifyPropertyChanged, INotifyPropertyChanging 
    {
        private object target;
        public object Target
        {
            get
            {
                return target;
            }
            set
            {
                OnPropertyChanging("Target");
                target = value;
                OnPropertyChanged("Target");
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OnPropertyChanging(string name)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
        }

        public DevicePageModel()
        {
        }
    }
}
