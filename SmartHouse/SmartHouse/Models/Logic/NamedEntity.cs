using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace SmartHouse.Models.Logic
{
    public class NamedEntity : BaseEntity
    {
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public NamedEntity()
        {

        }

        public NamedEntity(int id, string nameTemplate) : base(id)
        {
            if (nameTemplate.Contains("{"))
                Name = String.Format(nameTemplate, ID);
            else
                Name = nameTemplate;
        }
    }
}
