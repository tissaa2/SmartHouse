using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
using System.ComponentModel;
using SmartHouse.Models;
using System.Linq;
using System.Collections.Generic;

namespace SmartHouse.ViewModels
{

    public class GroupModel : IconNamedModel
    {
        private Group group = null;

        public Group Group
        {
            get => group;
            set
            {
                if(value != group)
                {
                    Target = group = value;
                }
            }
        }

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

        public int ID { get; set; } = -1;

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

        private List<DeviceModel> sources = null;
        public List<DeviceModel> Sources
        {
            get
            {
                if (sources == null)
                {
                    sources = Devices.Items.Where(e => e.IsInput).ToList();
                }
                return sources;
            }

            set => sources = value;
        }

        public override void Setup(params object[] args)
        {
            base.Setup(args);
            var t = Target as Group;
            if (t != null)
            {
                Group = t;
                devices = new ListViewModel<DeviceModel>(t.Project.Devices.Select(e => DeviceModel.CreateModel(e.Value) as DeviceModel).ToArray());
                scenes = new ListViewModel<SceneModel>(t.Scenes.Select(e => new SceneModel(e, this)).ToArray());
            }
        }

        public GroupModel(Group source): base(source)
        {
        }

        public override void Apply()
        {
            if (Parent is ProjectModel)
                throw new Exception("Project is null");
            var p = Parent as ProjectModel;

            Group g = Group;
            if (Group != null)
            {
                g = Group;
                if (IsDeleted)
                {
                    // Parent.Groups.Items.Remove(this);
                    p.Project.RemoveGroup(ID);
                    IsDirty = false;
                    return;
                }
            }
            else
            {
                Target = Group = g = p.Project.AddNewGroup();
            }
            base.Apply();
        }

    }
}
