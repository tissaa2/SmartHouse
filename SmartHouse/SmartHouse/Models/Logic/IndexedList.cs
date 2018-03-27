using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Models.CAN;
using System.Xml.Serialization;
using SmartHouse.Models.Core;

namespace SmartHouse.Models.Scenes
{
    public class IndexedList<IDType, IndexType, ItemType>: BaseEntity, IEnumerable<ItemType>, IIndexedListItem<IDType> where ItemType: IIndexedListItem<IndexType>
    {
        public IDType ID { get; set; }
        public List<ItemType> Items;

        [XmlIgnore]
        protected Dictionary<IndexType, ItemType> index = null;

        protected void CheckIndex()
        {
            if (index == null)
            {
                index = new Dictionary<IndexType, ItemType>();
                foreach (ItemType i in Items)
                {
                    index.Add(i.ID, i);
                }
            }
        }

        public ItemType this[IndexType id]
        {
            get
            {
                CheckIndex();
                return index[id];
            }
        }

        public IEnumerator<ItemType> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
