﻿using System.Collections;
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
            Items.CollectionChanged += ItemsChanged;
        }

        private void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Initialized)
                IsDirty = true;
        }


        // public IconListEntity(IDType id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        public IconListEntity(int id, string nameTemplate, string icon) : base(id, nameTemplate, icon)
        {
            Items.CollectionChanged += ItemsChanged;
        }


        private ObservableCollection<ItemType> items = new ObservableCollection<ItemType>();
        public ObservableCollection<ItemType> Items {
            get => items;
            set => CheckIsDirty(items, value, "Items", () => { items = value; });
        }

        public int Count => Items.Count;

        public bool IsReadOnly => false;

        //[XmlIgnore]
        //// private Dictionary<IndexType, ItemType> index = null;
        //private Dictionary<IndexType, ItemType> index = null;

        //protected void CheckIndex()
        //{
        //    if (index == null)
        //    {
        //        // index = new Dictionary<IndexType, ItemType>();
        //        index = new Dictionary<int, ItemType>();
        //        foreach (ItemType i in Items)
        //        {
        //            index.Add(i.ID, i);
        //        }
        //    }
        //}

        //public ItemType this[IndexType id]
        //{
        //    get
        //    {
        //        CheckIndex();
        //        return index[id];
        //    }
        //}

        /* public IEnumerator<ItemType> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        } */

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

        public ObservableCollection<ItemType> CreateModels()
        {
            ObservableCollection<ItemType> r = new ObservableCollection<ItemType>();
            foreach (var e in Items)
                r.Add( e.Clone() as ItemType);
            return r;
        }

        /* public ObservableCollection<ItemType> CreateModels()
        {
            ObservableCollection<ItemType> r = new ObservableCollection<ItemType>();
            foreach (var e in Items)
                r.Add(e.Clone() as ItemType);
            return r;
        } */

        /* public  void Save(string fileName)
        {

            // string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fn = Path.Combine(path, fileName);
            IconEntity<int> t = new IconEntity<int>() { Icon = "Icon.png", Name = "Name", ID = 128, SecurityLevel = 9 };
            using (StreamWriter writer = new StreamWriter(new FileStream(fn, FileMode.OpenOrCreate)))
            {
                XmlSerializer serializer = new XmlSerializer(t.GetType());

                serializer.Serialize(writer, t);
            }
        } */

    }
}
