using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.CAN;
using System.Xml.Serialization;

namespace SmartHouse.Models.Scenes
{
    public class Groups
    {
        private static Groups testItems = null;

        public static Groups TestItems
        {
            get
            {
                if (testItems == null)
                {
                    Populate();
                }
                return testItems;
            }
        }

        public static Group CG()
        {
            Random rnd = new Random();
            Group g = new Group();
            int j;
            int mi = rnd.Next(4, Device.Devices.Count);
            for (int i = 0; i < mi; i++)
            {
                Device d = Device.Devices[rnd.Next(0, Device.Devices.Count)];
                // тут я закончил
            }
            return g;
        }

        public static void Populate()
        {
            
            for (int i = 0; i < 4; i++)
            {

            }
        }

        public List<Group> Items;
    }
}
