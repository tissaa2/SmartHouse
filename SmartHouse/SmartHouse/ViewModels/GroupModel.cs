using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;
using System.Linq;

namespace SmartHouse.ViewModels
{

    public class GroupModel: IconNamedListViewModel<SceneModel> 
    {
        private ListViewModel<SceneModel> scenes = new ListViewModel<SceneModel>(null);
        public ListViewModel<SceneModel> Scenes
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

        private bool inputsMode = false;
        public bool InputsMode
        {
            get { return inputsMode; }
            set { inputsMode = value; OnPropertyChanged("InputsMode"); OnPropertyChanged("OutputsMode"); }
        }

        public bool OutputsMode
        {
            get { return !inputsMode; }
        }

        private bool devicesMode = false;
        public bool DevicesMode
        {
            get { return devicesMode; }
            set { devicesMode = value; OnPropertyChanged("DevicesMode"); OnPropertyChanged("AddButtonText"); OnPropertyChanged("ScenesMode"); }
        }

        public string AddButtonText
        {
            get { return devicesMode ? "Добавить устройство" : "Добавить сцену"; }
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

        public GroupModel(Group source)
        {
            Target = source;
            Icon = source.Icon;
            Name = source.Name;
        }

        public Group ToBusiness()
        {
            var g = new Group();
            Apply(g);
            return g;
        }

        public override void Apply(object target)
        {
            base.Apply(target);
            if (target is Group)
            {
                var g = target as Group;
                g.Items = Items.Select(e => { e.Apply(e.Target); return e.Target as Scene; }).ToList();
                g.Devices = Devices.Items.Select(;
            }
        }

    }
}
