using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
using System.Xml.Serialization;

namespace SmartHouse.Models.Logic
{
    public class Project : IconListEntity<int, int, Group>
    {
        public static Project Create(string name, string icon, int id)
        {
            // int bid = id * 10;
            return new Project()
            {
                Name = name,
                Icon = icon,
                ID = id,
                /* Items = new List<Group>() {
                            Group.Create("Прихожая", "hall.png", 1 + bid),
                            Group.Create("Зал", "livingroom.png", 2  + bid),
                            Group.Create("Кухня", "kitchen.png", 3 + bid),
                            Group.Create("Спальня", "bedroom.png", 4 + bid),
                            Group.Create("Туалет", "toilet.png", 5 + bid) */
                Items = new ObservableCollection<Group>() {
                            Group.Create("Прихожая", "hall.png", IntID.NewID()),
                            Group.Create("Зал", "livingroom.png", IntID.NewID()),
                            Group.Create("Кухня", "kitchen.png", IntID.NewID()),
                            Group.Create("Спальня", "bedroom.png", IntID.NewID()),
                            Group.Create("Туалет", "toilet.png", IntID.NewID())
                }
            };
        }
    }
}
