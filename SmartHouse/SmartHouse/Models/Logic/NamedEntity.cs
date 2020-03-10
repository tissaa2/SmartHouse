using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace SmartHouse.Models.Logic
{
    // public class NamedEntity<T> : BaseEntity<T>
    public class NamedEntity : BaseEntity
    {
        // [XmlIgnore]
        private string name = null;
        public virtual string Name
        {
            get => name;
            set
            {
                CheckIsDirty(name, value, "Name", () => { name = value; });
            }
        }


        public override string ToString()
        {
            return Name;
        }

        public NamedEntity()
        {

        }

        // public NamedEntity(T id, string nameTemplate) : base(id)
        public NamedEntity(int id, string nameTemplate) : base(id)
        {
            if (nameTemplate.Contains("{"))
                Name = String.Format(nameTemplate, ID);
            else
                Name = nameTemplate;
        }
    }
}
