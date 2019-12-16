using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using System.Linq;

namespace SmartHouse.ViewModels
{

    public class ScenePageModel: ListViewModel<DeviceModel> 
    {

        private object CheckIsDirty(object oldValue, object newValue, string eventName)
        {

            if (Object.Equals(oldValue, newValue))
                return oldValue;
            else
            {
                IsDirty = true;
                OnPropertyChanged(eventName);
                return newValue;
            }
        }

        private Device selectedDevice = null;
        public Device SelectedDevice
        {
            get
            {
                IsGroupEvent = selectedDevice is GroupSource;
                return selectedDevice;
                // return (Project)GetValue(SelectedItemProperty);
            }

            set
            {
                OnPropertyChanging("SelectedDevice");
                //selectedDevice = value;
                //OnPropertyChanged("SelectedDevice");
                selectedDevice = CheckIsDirty(selectedDevice, value, "SelectedDevice") as Device;
            }
        }

        private ObservableCollection<Device> devices;
        public ObservableCollection<Device> Devices
        {
            get
            {
                return devices;
                // return (Project)GetValue(SelectedItemProperty);
            }

            set
            {

                OnPropertyChanging("Devices");
                //devices = value;
                //OnPropertyChanged("Devices");
                devices = CheckIsDirty(devices, value, "Devices") as ObservableCollection<Device>;
            }
        }
        // закомментил, ибо надо будет добавить групповое событие в список источников устройств и определять тип события исходя и выбранности его
        //public bool IsGroupEvent { get => isGroupEvent; set { isGroupEvent = value; OnPropertyChanged("IsGroupEvent"); OnPropertyChanged("IsUIDEvent"); } }
        //public bool IsUIDEvent { get => !isGroupEvent; set { IsGroupEvent = !value; } }

        private bool isGroupEvent = false;
        public bool IsGroupEvent {
            get => isGroupEvent;
            set {
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
        public int InputID {
            get => inputID;
            set {
                // inputID = value;
                // OnPropertyChanged("InputID");
                inputID = (int)CheckIsDirty(inputID, value, "InputID");
            }
        }

        private byte groupID;
        public byte GroupID {
            get => groupID;
            set {
                //groupID = value;
                //OnPropertyChanged("GroupID");
                groupID = (byte)CheckIsDirty(groupID, value, "GroupID");
            }
        }

        private byte categoryID;
        public byte CategoryID {
            get => categoryID;
            set {
                //categoryID = value;
                //OnPropertyChanged("CategoryID");
                categoryID = (byte)CheckIsDirty(categoryID, value, "CategoryID");
            }
        }

        private byte timePar;
        public byte TimePar {
            get => timePar;
            set {
                //timePar = value;
                //OnPropertyChanged("TimePar");
                timePar = (byte)CheckIsDirty(timePar, value, "TimePar");
            }
        }

        private byte typeID;
        public byte TypeID {
            get => typeID;
            set {
                //typeID = value;
                //OnPropertyChanged("TypeID");
                typeID = (byte)CheckIsDirty(typeID, value, "TypeID");
            }
        }

        private string name;
        public string Name {
            get => name;
            set {
                //name = value;
                //OnPropertyChanged("Name");
                name = CheckIsDirty(name, value, "Name") as string;
            }
        }

        private string icon;
        public string Icon {
            get => icon;
            set {
                //icon = value;
                //OnPropertyChanged("Icon");
                icon = CheckIsDirty(icon, value, "Icon") as string;
            }
        }

        /* private string UID;
        public string UID { get => inputID; set { inputID = value; OnPropertyChanged("InputID"); } } */

        /* private Event _event = null;
        public Event Event { get => portNumber; set { portNumber = value; OnPropertyChanged("PortNumber"); } } */

        public ScenePageModel(ObservableCollection<DeviceModel> items/* , ViewEditTemplateSelector templateSelector */): base(items)
        {
            // Items = items;
        }
        логические устройства убрать из интерфейсов. вместо них юзать модели
        в DevicesListView поменять Device на Device model
        в Группе - то же.  
        в ScenePageModel - то же
        
        public void Assign(Scene scene, GroupPageModel groupPage)
        {
            Group g = groupPage.Target as Group;
            Target = scene;
            Devices = new ObservableCollection<Device>(g.Devices.Where(e => e.IsInput).Select(e => e));
            Devices.Insert(0, new GroupSource());
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
                this.SelectedDevice = Devices.FirstOrDefault(e => e is GroupSource);
            }
            else
            {
                var ev = scene.Event as UIDEvent;
                this.TypeID = ev.TypeID;
                // this.SelectedDevice = g.Devices.FirstOrDefault(e => e.UID == ev.UID && e.PortID == ev.InputID);
                this.SelectedDevice = Devices.FirstOrDefault(e => e.ID == ev.DeviceID);
            }
            IsDirty = false;
        }

        // в редакторе физ устройства сделать рабочий тип входа
        // там же пофиксить кнопку 'Привенить'. пофиксить ее вообще везде
        // > в редакторе сцены автоматически проставлять номер порта (поменять на 'входа') 
          
        public void Apply()
        {
            var t = Target as Scene;
            t.Items.Clear();
            foreach (var dm in Items)
                if (dm.Enabled)
                {
                    var st = new DeviceState() { ID = dm.Device.ID };
                    dm.Device.SetState(st);
                    t.Items.Add(st);
                }
            Event ev;
            // bool isGroupEvent = SelectedDevice is GroupSource;
            if (isGroupEvent)
            {
                var gev = new GroupEvent() { CategoryID = CategoryID, TimePar = TimePar, GroupID = GroupID };
                ev = gev;
            }
            else
            {
                int uid = 0;
                if (selectedDevice != null)
                    uid = selectedDevice.UID.Hash;
                // var uev = new UIDEvent() { UID = new Models.UID(uid), TypeID = TypeID };
                var uev = new UIDEvent() { DeviceID = selectedDevice.ID, TypeID = TypeID };
                ev = uev;
            }
            ev.InputID = (byte)InputID;
            t.Event = ev;
            t.Icon = icon;
            t.Name = name;
            IsDirty = false;
        }
    }
}
