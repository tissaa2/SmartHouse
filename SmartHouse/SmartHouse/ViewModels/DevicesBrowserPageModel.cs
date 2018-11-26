using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;
using SmartHouse.Models.Physics;

namespace SmartHouse.ViewModels
{

    public class DevicesBrowserPageModel: ListViewModel<PDevice> 
    {
        public DevicesBrowserPageModel(ObservableCollection<PDevice> items): base(items)
        {
        }
    }
}
