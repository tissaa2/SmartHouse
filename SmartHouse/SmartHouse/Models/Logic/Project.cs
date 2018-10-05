using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SmartHouse.Views;

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
                            Group.Create("Прихожая", "group_hall.png", IntID.NewID()),
                            Group.Create("Зал", "group_livingroom.png", IntID.NewID()),
                            Group.Create("Кухня", "group_kitchen.png", IntID.NewID()),
                            Group.Create("Спальня", "group_bedroom.png", IntID.NewID()),
                            Group.Create("Туалет", "group_toilet.png", IntID.NewID())
                }
            };
        }

        public Project()
        {
        }

        public Project(int id, string nameTemplate, string icon): base(id, nameTemplate, icon)
        {

        }

    }
}
