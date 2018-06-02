using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace SmartHouse.Models.Logic
{
    public class IconEntity<T>: NamedEntity<T>
    {
        public string Icon { get; set; }
    }
}
