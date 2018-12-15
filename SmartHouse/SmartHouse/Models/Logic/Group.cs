using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Logic;
// using SmartHouse.Models.Physics;

namespace SmartHouse.Models.Logic
{
    public class Group : IconListEntity<int, int, Scene>
    {
        public ObservableCollection<Group> Children { get; set; } = new ObservableCollection<Group>();
        public ObservableCollection<Device> Devices { get; set; } = new ObservableCollection<Device>();

        public void ReplaceDevice(Device oldDevice, Device newDevice)
        {
            Devices.Remove(oldDevice);
            Devices.Add(newDevice);
        }

        public static Group Create(string name, string icon, int id)
        {
            //int bid = id * 10;
            var g = new Group()
            {
                Name = name,
                Icon = icon,
                ID = id,
                Devices = new ObservableCollection<Device>()
                {
                    new Lamp("Люстра", 50),
                    new Lamp("Бра 1", 50),
                    new Lamp("Бра 2", 50),
                    new Lamp("Лунный свет", 50),
                    new Fan("Кондиционер", 50),
                    new Fan("Батареи", 50),
                    new Socket("Розетка у окна", true),
                    new Socket("Розетка у дивана", true),
                    new Socket("Розетка у двери", true),
                    new Switch("Выключатель у двери", true),
                    new Panel("Панель на стене", new bool[8]),
                    new MotionSensor("Датчик движени", new bool[4])
                }
            };

            g.Items = new ObservableCollection<Scene>()
                                {
                                    /* Scene.LightsOff(1 + bid),
                                    Scene.Night(2 + bid),
                                    Scene.SoftLight(3 + bid),
                                    Scene.BrightLight(4 + bid) */
                                    Scene.LightsOff(IntID.NewID(), g),
                                    Scene.Night(IntID.NewID(), g),
                                    Scene.SoftLight(IntID.NewID(), g),
                                    Scene.BrightLight(IntID.NewID(), g)
                                    /* new GroupEvent() { CategoryID = 1, GroupID = 1, Icon = "workLight.png", InputID = 0, Name = "Рабочий свет", SecurityLevel = 0, TimePar = 0},
                                    new UIDEvent() { Icon = "socket.png", InputID = 0, Name = "Розетка плиты", SecurityLevel = 1} */
                                };


            return g;
        }

        public Group()
        {

        }

        public Group(int id, string nameTemplate, string icon) : base (id, nameTemplate, icon)
        {
        }

    }
}
