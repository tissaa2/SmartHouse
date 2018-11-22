using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;

namespace SmartHouse.ViewModels
{

    public class DevicesBrowserPageModel: ListViewModel<Device> 
    {
        public DevicesBrowserPageModel(ObservableCollection<Device> items): base(items)
        {
        }
    }
}
