using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.Logic;
using System.Xml.Serialization;

namespace SmartHouse.Models.Logic
{
    public class Project : IconListEntity<int, int, Group>
    {
        public static Project Create(string name, string icon, int id)
        {
            int bid = id * 10;
            return new Project()
            {
                Name = name,
                Icon = icon,
                ID = id,
                Items = new List<Group>() {
                            Group.Create("Прихожая", "hall.png", 1 + bid),
                            Group.Create("Зал", "livingRoom.png", 2  + bid),
                            Group.Create("Кухня", "kitchen.png", 3 + bid),
                            Group.Create("Спальня", "bedroom.png", 4 + bid),
                            Group.Create("Туалет", "toilet.png", 5 + bid)
                }
            };
        }
    }
}
