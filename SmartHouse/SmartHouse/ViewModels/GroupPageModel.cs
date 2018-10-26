using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class GroupPageModel: ViewModel 
    {
        private ListViewModel<Scene> scenes = new ListViewModel<Scene>(null);
        public ListViewModel<Scene> Scenes
        {
            get
            {
                return scenes;
            }
            set
            {
                OnPropertyChanging("Scenes");
                scenes = value;
                OnPropertyChanged("Scenes");
            }
        }

        private bool devicesMode = false;
        public bool DevicesMode {
            get { return devicesMode; }
            set { devicesMode = value; OnPropertyChanged("DevicesMode"); OnPropertyChanged("ScenesMode"); }
        }

        public bool ScenesMode
        {
            get { return !devicesMode; }
            set { devicesMode = !value; OnPropertyChanged("DevicesMode"); OnPropertyChanged("ScenesMode"); }
        }

        private ListViewModel<DeviceModel> devices = new ListViewModel<DeviceModel>(null);
        public ListViewModel<DeviceModel> Devices
        {
            get
            {
                return devices;
            }
            set
            {
                OnPropertyChanging("Devices");
                devices = value;
                OnPropertyChanged("Devices");
            }
        }

        public GroupPageModel()
        {
        }
    }
}
