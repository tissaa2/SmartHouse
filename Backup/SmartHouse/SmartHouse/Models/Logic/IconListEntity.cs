using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Collections.Specialized;

namespace SmartHouse.Models.Logic
{
    // public class IconListEntity<IDType, IndexType, ItemType> : IconEntity<IDType>/*, IEnumerable<ItemType>, ICollection<ItemType> */, IUnique<IDType> where ItemType : BaseEntity<IndexType>
    public class IconListEntity<IndexType, ItemType> : IconEntity/*, IEnumerable<ItemType>, ICollection<ItemType> */, IUnique<int> where ItemType : BaseEntity
    {
        public IconListEntity()
        {
            // Items.CollectionChanged += ItemsChanged;
        }

        //private void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (Initialized)
        //        ProjectsList.SetIsDirty(true);
        //}


        // public IconListEntity(IDType id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        public IconListEntity(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
            // Items.CollectionChanged += ItemsChanged;
        }


        //private ObservableCollection<ItemType> items = new ObservableCollection<ItemType>();
        //public ObservableCollection<ItemType> Items
        //{
        //    get => items;
        //    set => CheckIsDirty(items, value, "Items", () => { items = value; });
        //}
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
