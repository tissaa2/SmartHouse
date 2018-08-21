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
                        instance = new ProjectsList();
                        instance.Items = new ObservableCollection<Project>() { 
                            Project.Create("Квартира", "project_flat.jpg", IntID.NewID()),
                            Project.Create("Дом", "project_house.jpg", IntID.NewID())
                        };
                        instance.Save();
                    }
                }
                return instance;
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
