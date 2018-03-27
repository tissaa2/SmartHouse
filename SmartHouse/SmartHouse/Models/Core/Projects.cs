using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.CAN;
using System.Xml.Serialization;

namespace SmartHouse.Models.Core
{
    public class Projects: IconListEntity<int, int, Project>
    {
        public static string FileName { get; set; } = "house.xml";

        private static Projects instance = null;

        private static Project A(string name, string icon, int id)
        {
            return new Group()
            {
                Name = name,
                Icon = icon,
                ID = id,
                Items = new List<Scene>()
                                /* {
                                    new Scene() {ID = new UID, Name}
                                    new GroupEvent() { CategoryID = 1, GroupID = 1, Icon = "switchOff.png", InputID = 0, Name = "Выключить все", SecurityLevel = 0, TimePar = 0},
                                    new GroupEvent() { CategoryID = 1, GroupID = 1, Icon = "softLight.png", InputID = 0, Name = "Мягкий свет", SecurityLevel = 0, TimePar = 0},
                                    new GroupEvent() { CategoryID = 1, GroupID = 1, Icon = "brightLight.png", InputID = 0, Name = "Полный свет", SecurityLevel = 0, TimePar = 0},
                                    new GroupEvent() { CategoryID = 1, GroupID = 1, Icon = "nightLight.png", InputID = 0, Name = "Ночной свет", SecurityLevel = 0, TimePar = 0},
                                    new GroupEvent() { CategoryID = 1, GroupID = 1, Icon = "workLight.png", InputID = 0, Name = "Рабочий свет", SecurityLevel = 0, TimePar = 0},

                                    new UIDEvent() { Icon = "switch.png", InputID = 0, Name = "Выключатель рабочей зоны", SecurityLevel = 0},
                                    new UIDEvent() { Icon = "fan.png", InputID = 0, Name = "Вентилятор", SecurityLevel = 0},
                                    new UIDEvent() { Icon = "socket.png", InputID = 0, Name = "Розетка плиты", SecurityLevel = 1}
                                }
            };
        }

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
                        instance.Groups = new List<Group>() {
                            A("Прихожая", "hall.png", 1),
                            A("Зал", "livingRoom.png", 2),
                            A("Кухня", "kitchen.png", 3),
                            A("Спальня", "bedroom.png", 4),
                            A("Туалет", "toilet.png", 4)
                        };

                    }
                }
                return instance;
            }
        }
        public List<Group> Groups { get; set; }
        public void Save()
        {
            Save(FileName);
        }

    }
}
