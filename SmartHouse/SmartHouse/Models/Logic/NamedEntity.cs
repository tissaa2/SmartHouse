using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace SmartHouse.Models.Logic
{
    public class NamedEntity<T>: BaseEntity<T>
    {
        public string Name { get; set; }
    }
}
