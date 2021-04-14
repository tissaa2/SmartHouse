﻿using System;
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

    public class ProjectModel : IconNamedListViewModel<GroupModel>
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
            }
        }

        public ProjectModel() : base()
        {
        }

        public ProjectModel(Project target) : base(target.Items.Select(e => new GroupModel(e)).ToArray(), target.Icon, target.Name) 
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

        public override void Apply(object target)
        {
            base.Apply(target);
            if (target is Project)
            {
                var p = target as Project;
                GroupModel
                p.Items = Items.Select(e => e.;
            }
        }
    }
}
