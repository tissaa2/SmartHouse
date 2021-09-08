using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
using System.ComponentModel;
using SmartHouse.Models;
using SmartHouse.Models.Physics;
using Xamarin.Forms;

namespace SmartHouse.ViewModels
{

    public class DevicesBrowserModel: IconNamedModel 
    {
        private ListViewModel<PDevice> devices = new ListViewModel<PDevice>(null);
        public ListViewModel<PDevice> Devices
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

        private Port selectedPort = null;
        public Port SelectedPort
        {
            get => selectedPort;
            set
            {
                if (selectedPort != null)
                    selectedPort.BGColor = Color.Transparent;
                selectedPort = value;
                selectedPort.BGColor = Color.FromHex("DDDDEE");
                selectedPort.Parent.Fold = false;
                OnPropertyChanged("SelectedPort");
                OnPropertyChanged("SelectButtonVisible");
                Devices.SelectedItem = selectedPort.Parent;
            }
        }

        public bool SelectButtonVisible
        {
            get
            {
                return selectedPort != null;
            }
        }

        public DevicesBrowserModel(ObservableCollection<PDevice> items): base("Devices browser", null, items)
        {
        }
    }
}
