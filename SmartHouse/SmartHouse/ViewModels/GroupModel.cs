﻿using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.ComponentModel;
using SmartHouse.Models;
using System.Linq;
using System.Collections.Generic;

namespace SmartHouse.ViewModels
{

    // public class GroupModel : IconNamedListViewModel<SceneModel>
    public class GroupModel : IconNamedModel
    {
        public Project Project { get; set; }

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

        //TODO: как быть, если элемент был добавлен в список ViewModel? В этом случае у него не будет сущности бизнес-логики        
        public override void Apply(object target)
        {
            Group g = null; 
            if (target is Group)
            {
                g = target as Group;
                if (IsDeleted)
                {
                    if (Project != null)
                    {
                        Project.Items.Remove(g);
                    }
                    else
                        throw new Exception("Project is null");
                    IsDirty = false;
                    return;
                }
            }
            else
            {
                if (Project != null)
                {
                    g = Project.AddNewGroup();
                }
            }
            base.Apply(g);
            g.DeviceIDs = Devices.Items.Select(e => e.ID).ToList();
            g.Scenes = Scenes.Items.Select(e => e.Target).ToList() обдумать
        }

    }
}
