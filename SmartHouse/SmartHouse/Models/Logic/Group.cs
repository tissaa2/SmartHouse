using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using SmartHouse.Models.Logic;
// using SmartHouse.Models.Physics;

namespace SmartHouse.Models.Logic
{
    public class Group : IconListEntity<int, Scene>
    {
        public List<Group> Children { get; set; } = new List<Group>();
        public List<int> DeviceIDs { get; set; } = new List<int>();

        public static Group Create(Project parent, string name, string icon, int id)
        {
            ////int bid = id * 10;
            //var uid0 = Group.UIDID.NewID();
            //var uid1 = Group.UIDID.NewID();
            //var g = new Group()
            //{
            //    Name = name,
            //    Icon = icon,
            //    ID = id,
            //    Devices = new ObservableCollection<Device>()
            //    {
            //        new Lamp("Люстра", 50, uid0, 0),
            //        new Lamp("Бра 1", 50, uid0, 1),
            //        new Lamp("Бра 2", 50, uid0, 2),
            //        new Lamp("Лунный свет", 50, uid0, 3),
            //        new Fan("Кондиционер", 50, uid0, 4),
            //        new Fan("Батареи", 50, uid0, 5),
            //        new Socket("Розетка у окна", true, uid1, 0),
            //        new Socket("Розетка у дивана", true, uid1, 1),
            //        new Socket("Розетка у двери", true, uid1, 2),
            //        new Switch("Выключатель у двери", true, uid1, 3),
            //        new Panel("Панель на стене", new bool[8], Group.UIDID.NewID(), 0),
            //        new MotionSensor("Датчик движени", new bool[4], Group.UIDID.NewID(), 0)
            //    }
            //};

            //g.Items = new ObservableCollection<Scene>()
            //                    {
            //                        /* Scene.LightsOff(1 + bid),
            //                        Scene.Night(2 + bid),
            //                        Scene.SoftLight(3 + bid),
            //                        Scene.BrightLight(4 + bid) */
            //                        Scene.LightsOff(IntID.NewID(), g),
            //                        Scene.Night(IntID.NewID(), g),
            //                        Scene.SoftLight(IntID.NewID(), g),
            //                        Scene.BrightLight(IntID.NewID(), g)
            //                        /* new GroupEvent() { CategoryID = 1, GroupID = 1, Icon = "workLight.png", InputID = 0, Name = "Рабочий свет", SecurityLevel = 0, TimePar = 0},
            //                        new UIDEvent() { Icon = "socket.png", InputID = 0, Name = "Розетка плиты", SecurityLevel = 1} */
            //                    };


            //int bid = id * 10;
            // var uid0 = new UID(0x189);
            var g = new Group()
            {
                Name = name,
                Icon = icon,
                ID = id,
                DeviceIDs = new List<int>()
                {
                    1,
                    2,
                    3
                }
            };

            g.Items = new List<Scene>()
                                {
                                    new Scene("Выключить все", "scene_switchoff.png",
                                         new UIDEvent(2, 2),
                                         new  List<DeviceState>(){ new DeviceState(1, "0")}, 0),
                                    new Scene("Полный свет", "scene_brightlight.png",
                                         new UIDEvent(3, 3),
                                         new  List<DeviceState>(){new DeviceState(1, "100")}, 100)
                                };
            return g;
        }

        private void Setup()
        {
        }

        public Group()
        {
            Setup();
        }

        public Group(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
            Setup();
        }

        public override void Init()
        {
            base.Init();
            foreach (var e in Devices)
                e.Init();
        }


    }
}
