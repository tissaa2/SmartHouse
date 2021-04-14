using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using System.Linq;

namespace SmartHouse.ViewModels
{

    public class SceneModel : IconNamedListViewModel<DeviceStateModel>
    {
        private DeviceModel selectedSource = null;
        public DeviceModel SelectedSource
        {
            get
            {
                IsGroupEvent = selectedSource is GroupSourceModel;
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
                // return (Project)GetValue(SelectedItemProperty);
            }

            set
            {

                OnPropertyChanging("Sources");
                //devices = value;
                //OnPropertyChanged("Devices");
                // devices = CheckIsDirty(devices, value, "Devices") as ObservableCollection<DeviceModel>;
                CheckIsDirty(sources, value, "Sources", () => sources = value);
            }
        }
        // закомментил, ибо надо будет добавить групповое событие в список источников устройств и определять тип события исходя и выбранности его
        //public bool IsGroupEvent { get => isGroupEvent; set { isGroupEvent = value; OnPropertyChanged("IsGroupEvent"); OnPropertyChanged("IsUIDEvent"); } }
        //public bool IsUIDEvent { get => !isGroupEvent; set { IsGroupEvent = !value; } }

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

        private byte typeID;
        public byte TypeID
        {
            get => typeID;
            set
            {
                //typeID = value;
                //OnPropertyChanged("TypeID");
                // typeID = (byte)CheckIsDirty(typeID, value, "TypeID");
                CheckIsDirty(typeID, value, "TypeID", () => typeID = value);
            }
        }

        /* private string UID;
        public string UID { get => inputID; set { inputID = value; OnPropertyChanged("InputID"); } } */

        /* private Event _event = null;
        public Event Event { get => portNumber; set { portNumber = value; OnPropertyChanged("PortNumber"); } } */

        public SceneModel(Scene source) : base(source.Items.Select(e => DeviceModel.CreateModel(e) as DeviceModel).ToArray(), source.Icon, source.Name)
        {
            // Items = items;
        }


        public Scene ToBusiness()

        {
            var s = new Scene();
            Apply(s);
            return s;
        }

        private void DeviceStateChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        public void Assign(Scene scene, GroupModel groupPage)
        {
            Group g = groupPage.Target as Group;
            Target = scene;
            // Devices = new ObservableCollection<DeviceModel>(g.Devices.Where(e => e.IsInput).Select(e => e));
            Sources = new ObservableCollection<DeviceModel>(g.Devices.Where(i => i.IsInput == true).Select(i =>
            {
                var m = DeviceModel.CreateModel(i) as DeviceModel;
                m.Enabled = true;
                m.Group = g;
                m.PropertyChanged += DeviceStateChanged;
                return m;
            }));
            Sources.Insert(0, new GroupSourceModel());
            this.GroupID = (byte)g.ID;
            this.InputID = scene.Event.InputID;
            IsGroupEvent = scene.Event is GroupEvent;
            Icon = scene.Icon;
            Name = scene.Name;
            if (IsGroupEvent)
            {
                var ev = scene.Event as GroupEvent;
                this.TimePar = ev.TimePar;
                this.CategoryID = ev.CategoryID;
                this.SelectedSource = Sources.FirstOrDefault(e => e is GroupSourceModel);
            }
            else
            {
                var ev = scene.Event as UIDEvent;
                this.TypeID = ev.TypeID;
                // this.SelectedDevice = g.Devices.FirstOrDefault(e => e.UID == ev.UID && e.PortID == ev.InputID);
                this.SelectedSource = Sources.FirstOrDefault(e => e.ID == ev.DeviceID);
            }
            IsDirty = false;
        }

        // в редакторе физ устройства сделать рабочий тип входа
        // там же пофиксить кнопку 'Привенить'. пофиксить ее вообще везде
        // > в редакторе сцены автоматически проставлять номер порта (поменять на 'входа') 

        public override void Apply(object target)
        {
            base.Apply(target);
            var t = target as Scene;
            t.Items.Clear();
            foreach (var dm in Items)
                if (dm.Enabled)
                {
                    var st = new DeviceState() { DeviceID = dm.Device.ID, Value = dm.State };
                    t.Items.Add(st);
                }
            Event ev;
            if (isGroupEvent)
            {
                var gev = new GroupEvent() { CategoryID = CategoryID, TimePar = TimePar, GroupID = GroupID };
                ev = gev;
            }
            else
            {
                int uid = 0;
                if (selectedSource != null)
                    uid = selectedSource.Device.UID.Hash;
                var uev = new UIDEvent() { DeviceID = selectedSource.ID, TypeID = TypeID };
                ev = uev;
            }
            ev.InputID = (byte)InputID;
            t.Event = ev;
            t.Icon = Icon;
            t.Name = name;
            IsDirty = false;
        }
    }
}
