using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;
using SmartHouse.Models.Physics;
using Xamarin.Forms;

namespace SmartHouse.ViewModels
{

    public class DevicesBrowserPageModel: ListViewModel<PDevice> 
    {
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
                SelectedItem = selectedPort.Parent;
            }
        }

        public bool SelectButtonVisible
        {
            get
            {
                return selectedPort != null;
            }
        }

        public DevicesBrowserPageModel(ObservableCollection<PDevice> items): base(items)
        {
        }
    }
}
