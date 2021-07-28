﻿using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Storage;
using System.ComponentModel;
using SmartHouse.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartHouse.ViewModels
{

    public class ProjectModel : IconNamedModel
    {
        public Project Project { get; set; }

        private ListViewModel<GroupModel> groups = new ListViewModel<GroupModel>(null);
        public ListViewModel<GroupModel> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                CheckIsDirty(groups, value, "Groups", () => { groups = value; });
            }
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
                CheckIsDirty(devices, value, "Devices", () => { devices = value; });
            }
        }

        public ProjectModel() : base()
        {
        }

        public ProjectModel(Project target) : base(target.Icon, target.Name) 
        {
            Groups = new ListViewModel<GroupModel>(target.Groups.Values.Select(e => new GroupModel(e)).ToArray()); 
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
            Project
            if (target is Project)
            {
                var p = target as Project;
                if (p.)
                p.Items = Items.Select(e => e.;
            }
            base.Apply();
        }
    }
}
