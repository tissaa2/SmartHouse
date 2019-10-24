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
        private Device selectedDevice = null;
        public Device SelectedDevice
        {
            get
            {
                return selectedDevice;
                // return (Project)GetValue(SelectedItemProperty);
            }

            set
            {
                OnPropertyChanging("SelectedDevice");
                selectedDevice = value;
                OnPropertyChanged("SelectedDevice");
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
                devices = value;
                OnPropertyChanged("Devices");
            }
        }

        private bool isGroupEvent = false;
        public bool IsGroupEvent { get => isGroupEvent; set { isGroupEvent = value; OnPropertyChanged("IsGroupEvent"); OnPropertyChanged("IsUIDEvent"); } }

        public bool IsUIDEvent { get => !isGroupEvent; set { IsGroupEvent = !value; } }

        private int inputID;
        public int InputID { get => inputID; set { inputID = value; OnPropertyChanged("InputID"); } }

        private byte groupID;
        public byte GroupID { get => groupID; set { groupID = value; OnPropertyChanged("GroupID"); } }

        private byte categoryID;
        public byte CategoryID { get => categoryID; set { categoryID = value; OnPropertyChanged("CategoryID"); } }

        private byte timePar;
        public byte TimePar { get => timePar; set { timePar = value; OnPropertyChanged("TimePar"); } }

        private byte typeID;
        public byte TypeID { get => typeID; set { typeID = value; OnPropertyChanged("TypeID"); } }

        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }

        private string icon;
        public string Icon { get => icon; set { icon = value; OnPropertyChanged("Icon"); } }

        /* private string UID;
        public string UID { get => inputID; set { inputID = value; OnPropertyChanged("InputID"); } } */

        /* private Event _event = null;
        public Event Event { get => portNumber; set { portNumber = value; OnPropertyChanged("PortNumber"); } } */

        public ScenePageModel(ObservableCollection<DeviceModel> items/* , ViewEditTemplateSelector templateSelector */): base(items)
        {
            // Items = items;
        }

        public void Assign(Scene scene, GroupPageModel groupPage)
        {
            Group g = groupPage.Target as Group;
            Target = scene;
            Devices = new ObservableCollection<Device>(g.Devices.Where(e => e.IsInput).Select(e => e));
            this.GroupID = (byte)g.ID;
            this.InputID = scene.Event.InputID;
            this.IsGroupEvent = scene.Event is GroupEvent;
            Icon = scene.Icon;
            Name = scene.Name;
            if (isGroupEvent)
            {
                var ev = scene.Event as GroupEvent;
                this.TimePar = ev.TimePar;
                this.CategoryID = ev.CategoryID;
            }
            else
            {
                var ev = scene.Event as UIDEvent;
                this.TypeID = ev.TypeID;
                this.SelectedDevice = g.Devices.FirstOrDefault(e => e.ID == ev.UID);
            }
            IsDirty = false;
        }

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
                var uev = new UIDEvent() {UID =  new Models.UID(uid), TypeID = TypeID};
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
