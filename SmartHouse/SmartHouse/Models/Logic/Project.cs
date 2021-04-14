using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SmartHouse.Views;
using System.Collections.Generic;

namespace SmartHouse.Models.Logic
{
    // public class Project : IconListEntity<int, int, Group>
    public class Project : IconListEntity<int, Group>
    {

        public static Project Create(string name, string icon, int id)
        {
            var uid0 = new UID(0x2f9);
            int lampID = Lamp.IntID.NewID();
            int offSwitchID = Switch.IntID.NewID();
            int onSwitchID = Switch.IntID.NewID();
            var result = new Project()
            {
                Name = name,
                Icon = icon,
                ID = id,
                //Items = new ObservableCollection<Group>() {
                //            Group.Create("Прихожая", "group_hall.png", IntID.NewID()),
                //            Group.Create("Зал", "group_livingroom.png", IntID.NewID()),
                //            Group.Create("Кухня", "group_kitchen.png", IntID.NewID()),
                //            Group.Create("Спальня", "group_bedroom.png", IntID.NewID()),
                //            Group.Create("Туалет", "group_toilet.png", IntID.NewID())
                //}

                Devices = new Dictionary<int, Device>() {
                    {lampID, new Lamp(lampID, "Люстра", 50, uid0, 6) },
                    {offSwitchID, new Switch(offSwitchID, "Кнопка выкл", true, uid0, 2) },
                    {onSwitchID, new Switch(onSwitchID, "Кнопка вкл", true, uid0, 3) } }
            };

            result.Items = new List<Group>() {
                              Group.Create(result, "Тронный зал", "group_toilet.png", IntID.NewID())
                };
            return result;
        }

        /// <summary>
        /// Девайсы у нас лежат в проекте, а все остальные на них ссылаются
        /// </summary>
        public Dictionary<int, Device> Devices { get; set; }


        public Project()
        {
        }

        public Project(int id, string nameTemplate, string icon): base(id, nameTemplate, icon)
        {

        }

        public override void Clear()
        {
            Items.Clear();
        }

        public override void Init()
        {
            base.Init();
            foreach (var e in Items)
                e.Init();
        }

    }
}
