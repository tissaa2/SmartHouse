using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace SmartHouse.ViewModels
{

    public class SceneModel : IconNamedModel
    {
        public SceneModel(Scene target): base(target.Name, target.Icon, target)
        {
            Scene = target;
        }

        public Scene Scene { get; set; }
        private DeviceModel selectedSource = null;
        public DeviceModel SelectedSource
        {
            get
            {
                IsGroupEvent = selectedSource.DeviceType == DeviceType.Group;
                return selectedSource;
            }

            set
            {
                OnPropertyChanging("SelectedSource");
                CheckIsDirty(selectedSource, value, "SelectedSource", () => selectedSource = value);
            }
        }

        private ObservableCollection<DeviceModel> sources;
        public ObservableCollection<DeviceModel> Sources
        {
            get
            {
                return sources;
            }

            set
            {

                OnPropertyChanging("Sources");
                CheckIsDirty(sources, value, "Sources", () => sources = value);
            }
        }

        private bool isGroupEvent = false;
        public bool IsGroupEvent
        {
            get => isGroupEvent;
            set
            {
                if (isGroupEvent != value)
                {
                    isGroupEvent = value;
                    OnPropertyChanged("IsGroupEvent");
                    OnPropertyChanged("IsUIDEvent");
                    IsDirty = true;
                }
            }
        }

        public bool IsUIDEvent { get => !isGroupEvent; set { IsGroupEvent = !value; } }

        private int inputID;
        public int InputID
        {
            get => inputID;
            set
            {
                CheckIsDirty(inputID, value, "InputID", () => inputID = value);
            }
        }

        private byte groupID;
        public byte GroupID
        {
            get => groupID;
            set
            {
                CheckIsDirty(groupID, value, "GroupID", () => groupID = value);
            }
        }

        private byte categoryID;
        public byte CategoryID
        {
            get => categoryID;
            set
            {
                CheckIsDirty(categoryID, value, "CategoryID", () => categoryID = value);
            }
        }

        private byte timePar;
        public byte TimePar
        {
            get => timePar;
            set
            {
                CheckIsDirty(timePar, value, "TimePar", () => timePar = value);
            }
        }

        private byte inputTypeID;
        public byte InputTypeID
        {
            get => inputTypeID;
            set
            {
                CheckIsDirty(inputTypeID, value, "InputTypeID", () => inputTypeID = value);
            }
        }

        public List<DeviceStateModel> States { get; set;}

        private void DeviceStateChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        public void Assign(Scene scene, GroupModel groupModel)
        {
            var g = groupModel.Group;
            Sources = new ObservableCollection<DeviceModel>(groupModel.Devices.Items.Select(e =>
            {
                var dm = e.Clone() as DeviceModel; 
                dm.PropertyChanged += DeviceStateChanged;
                return dm;
            }));
            Sources.Insert(0, new GroupSourceModel());
            this.GroupID = (byte)g.ID;
            this.InputID = scene.Event.InputID;
            IsGroupEvent = scene.Event.Type == EventType.GroupEvent;
            Icon = scene.Icon;
            Name = scene.Name;
            if (IsGroupEvent)
            {
                var ev = scene.Event;
                this.TimePar = ev.TimePar;
                this.CategoryID = ev.CategoryID;
                this.SelectedSource = Sources.FirstOrDefault(e => e is GroupSourceModel);
            }
            else
            {
                var ev = scene.Event;
                this.InputTypeID = ev.InputTypeID;
                // this.SelectedDevice = g.Devices.FirstOrDefault(e => e.UID == ev.UID && e.PortID == ev.InputID);
                this.SelectedSource = Sources.FirstOrDefault(e => e.UID == ev.UID.ToString());
            }
            IsDirty = false;
        }

        public override void Apply()
        {
            Scene.States = States.Select(e => new DeviceState() { DeviceID = e.DeviceID, Value = e.State }).ToList();
            Event ev;
            if (isGroupEvent)
            {
                ev = Event.GroupEvent((byte)InputID, GroupID, CategoryID, TimePar);
            }
            else
            {
                ev = Event.UIDEvent(new Models.UID(selectedSource.UID), byte.Parse(selectedSource.PortID), InputTypeID);
            }
            ev.InputID = (byte)InputID;
            Scene.Event = ev;
            Scene.Icon = Icon;
            Scene.Name = name;
            base.Apply();
        }
    }
}
