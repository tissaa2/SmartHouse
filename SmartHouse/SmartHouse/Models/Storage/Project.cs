using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SmartHouse.Views;
using System.Collections.Generic;

namespace SmartHouse.Models.Storage
{
    public class Project: IconNamedEntity
    {

        public static Project Create(string name, string icon, int id)
        {
            var uid0 = new UID(0x2f9);
            int lampID = ProjectsList.Instance.IntID.NewID();
            int offSwitchID = ProjectsList.Instance.IntID.NewID();
            int onSwitchID = ProjectsList.Instance.IntID.NewID();
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
                    {lampID, new Device(DeviceType.Lamp, lampID, uid0,  6, "Люстра", "device_lamp.png") },
                    {offSwitchID, new Device(DeviceType.Switch, offSwitchID, uid0, 2, "Кнопка выкл", "device_switch.png") },
                    {onSwitchID, new Device(DeviceType.Switch, onSwitchID, uid0, 3, "Кнопка вкл", "device_switch.png") } 
                }
            };

            var gid = ProjectsList.Instance.IntID.NewID();
            result.Groups = new Dictionary<int, Group>() {
                {
                    gid,
                    Group.Create(result, "Тронный зал", "group_toilet.png", gid) 
                }
            };
            return result;
        }

        /// <summary>
        /// Девайсы у нас лежат в проекте, а все остальные на них ссылаются
        /// </summary>
        public Dictionary<int, Device> Devices { get; set; }
        public Dictionary<int, Group> Groups { get; set; }


        public Project()
        {
        }

        public Project(int id, string nameTemplate, string icon): base(id, nameTemplate, icon)
        {

        }

        public void Clear()
        {
            Groups.Clear();
        }

        public int NextGroupID { get; set; } = 0;
        public object groupLocker = new object();


        public Group AddNewGroup()
        {
            lock (groupLocker)
            {

                var g = new Group() { ID = ProjectsList.Instance.IntID.NewID() };

                Groups.Add(g.ID, g);
                return g;
            }
        }

        public void RemoveGroup(int id)
        {
            lock (groupLocker)
            {

                Groups.Remove(id);
            }
        }

    }
}
