using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Models
{
    public class IconNameAttribute: Attribute
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public IconNameAttribute(string icon, string name)
        {
            Name = name;
            Icon = icon;
        }
    }
}
