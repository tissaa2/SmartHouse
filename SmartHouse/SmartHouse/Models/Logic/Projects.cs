using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Physics;
using System.Xml.Serialization;

namespace SmartHouse.Models.Logic
{
    public class Projects: IconListEntity<int, int, Project>
    {
        public static string FileName { get; set; } = "projects.xml";

        private static Projects instance = null;

        public static Projects Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Load<Projects>(FileName);
                    if (instance == null)
                    {
                        instance = new Projects();
                        instance.Items = new List<Project>() { 
                            Project.Create("Квартира", "flat.png", 1),
                            Project.Create("Дом", "house.png", 2)
                        };

                    }
                }
                return instance;
            }
        }

        public void Save()
        {
            Save(FileName);
        }
    }
}
