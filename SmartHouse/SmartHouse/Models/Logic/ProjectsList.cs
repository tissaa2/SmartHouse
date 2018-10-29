using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Physics;
using System.Xml.Serialization;

namespace SmartHouse.Models.Logic
{
    public class ProjectsList: IconListEntity<int, int, Project>
    {
        public static string FileName { get; set; } = "projects.xml";
        public static ProjectsList TestData {
            get {
                return new ProjectsList()
                {
                    Items = new ObservableCollection<Project>() {
                            Project.Create("Квартира", "project_flat.png", IntID.NewID()),
                            Project.Create("Дом", "project_house.png", IntID.NewID()) }
                };
            }
        }

        public static void LoadTestData()
        {
            instance = TestData;
        }

        private static ProjectsList instance = null;
        public static ProjectsList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Load<ProjectsList>(FileName);
                    if (instance == null)
                    {
                        LoadTestData();
                        instance.Save();
                    }
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public ProjectsList()
        {

        }

        public void Save()
        {
            Save(FileName);
        }
    }
}
