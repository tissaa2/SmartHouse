using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Collections.Specialized;

namespace SmartHouse.Models.Logic
{
    public class IconListEntity<IndexType, ItemType> : NamedIconEntity, IUnique<int> where ItemType : BaseEntity
    {
        public IconListEntity()
        {
            // Items.CollectionChanged += ItemsChanged;
        }

        public IconListEntity(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
        }
        public List<ItemType> Items { get; set; } = new List<ItemType>();
 
        public int Count => Items.Count;

        public bool IsReadOnly => false;

        public void Add(ItemType item)
        {
            Items.Add(item);
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ItemType item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ItemType[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ItemType item)
        {
            throw new NotImplementedException();
        }


    }
}
