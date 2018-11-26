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
        // [XmlIgnore]
        private string name;

        // [XmlAttribute("Name")]
        public virtual string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public override string ToString()
        {
            return Name;
        }

        public NamedEntity()
        {

        }

        public NamedEntity(T id, string nameTemplate): base(id)
        {
            if (nameTemplate.Contains("{"))
                Name = String.Format(nameTemplate, ID);
            else
                Name = nameTemplate;
        }
    }
}
