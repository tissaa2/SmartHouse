using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartHouse.ViewModels
{

    public class ProjectModel : IconNamedListViewModel<Group>
    {

        private ListViewModel<DeviceModel> devices = new ListViewModel<DeviceModel>(null);
        public ListViewModel<DeviceModel> Devices
        {
            get
            {
                return devices;
            }
            set
            {
                CheckIsDirty(devices, value, "Devices", () => { devices = value; });
                //OnPropertyChanging("Devices");
                //devices = value;
                //OnPropertyChanged("Devices");
            }
        }

        public ProjectModel() : base()
        {
        }

        public ProjectModel(ICollection<Group> items, string icon, string name) : base(items, icon, name) 
        {
        }

        public override void Assign(ViewModel source)
        {
            base.Assign(source);
            if (source is ProjectModel)
            {
                var p = source as ProjectModel;
                Devices = new ListViewModel<DeviceModel>(p.Devices.Items);
            }
        }

        public override void Apply()
        {
            base.Apply();
            if (Target is Project)
            {
                var p = Target as Project;
                //List<Group> i2add = new List<Group>();
                //foreach (var e in Items)
                //{
                //    if (p.Items.FirstOrDefault(i => i.ID == e.ID) == null)
                //        i2add.Add(e)
                //}
                p.Items = Items;
            }
        }
    }
}
