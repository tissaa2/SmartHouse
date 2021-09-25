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

        public List<DeviceModel> Sources
        {
            get;
            set;
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

        private int groupID;
        public int GroupID
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

        public SceneModel(Scene scene, GroupModel groupModel): base(scene, groupModel)
        {
        }

        public override void Setup(params object[] args)
        {
            base.Setup(args);
            var scene = Target as Scene;
            var groupModel = args[1] as GroupModel;

            var g = groupModel.Group;
            Sources = groupModel.Sources;
            this.GroupID = g.ID;
            this.InputID = scene.Event.InputID;
            IsGroupEvent = scene.Event.Type == EventType.GroupEvent;
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
                this.SelectedSource = Sources.FirstOrDefault(e => e.UID == ev.UID.ToString());
            }
        }

        public override void Apply()
        {
            Scene.States = States.Select(e => new DeviceState() { DeviceID = e.DeviceID, Value = e.State }).ToList();
            Event ev;
            if (isGroupEvent)
            {
                ev = Event.GroupEvent((byte)InputID, (byte)GroupID, CategoryID, TimePar);
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
