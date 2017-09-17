using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models
{
    public class GroupItem
    {
        private static List<GroupItem> testItems = null;
        public static List<GroupItem> TestItems
        {
            get
            {
                if (testItems == null)
                {
                    testItems = new List<GroupItem>() {
                        new GroupItem("kitchen.png", "Кухня"),
                        new GroupItem("bedroom.png", "Спальня"),
                        new GroupItem("livingroom.png", "Гостиная")
                    };
                }
                return testItems;
            }
        }

        public string Picture { get; set; }
        public string Text { get; set; }

        public GroupItem(string picture, string text)
        {
            Picture = picture;
            Text = text;
        }
    }
}
