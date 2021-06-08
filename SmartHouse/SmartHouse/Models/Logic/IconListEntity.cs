using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Collections.Specialized;

namespace SmartHouse.Models.Logic
{
    public class IconListEntity<ItemType> : IconNamedEntity, IUnique<int> where ItemType : BaseEntity
    {
        public IconListEntity()
        {
        }

        public IconListEntity(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
        }

        public List<ItemType> Items { get; set; } = new List<ItemType>();

    }
}
