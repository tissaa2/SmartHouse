using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Physics;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{
    // public class ProjectsList : IconListEntity<int, int, Project>
    public class ProjectsList : IconListEntity<Project>
    {

        public static string FileName { get; set; } = "projects.json";
        public static ProjectsList TestData {
            get {
                return new ProjectsList()
                {
                    //Items = new ObservableCollection<Project>() {
                    //        Project.Create("Квартира", "project_flat.png", IntID.NewID()),
                    //        Project.Create("Дом", "project_house.png", IntID.NewID()) }
                    Items = new List<Project>() { Project.Create("Офис", "project_flat.png", IntID.NewID())}
                };
            }
        }

        public static void LoadTestData()
        {
            instance = TestData;
        }

        private static ProjectsList instance = null;
        private static bool initializing = false;
        public static ProjectsList Instance
        {
            get
            {
                if (instance == null)
                {
                    initializing = true;
                    instance = Load<ProjectsList>(FileName);
                    if (instance == null)
                    {
                        LoadTestData();
                        instance.Save();
                    }
                    if (instance != null)
                    {
                        instance.Init();
                    }
                    initializing = false;
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }
        public bool IsDirty { get; set; } = false;

        public static void SetIsDirty(bool value)
        {
            if (initializing)
                return;
            instance.IsDirty = value;
        }

        public ProjectsList()
        {

        }

        public void Save()
        {
            Save(FileName);
        }

        public override void Init()
        {
            base.Init();
            foreach(var e in Items)
            {
                e.Init();
            }
        }
    }
}
