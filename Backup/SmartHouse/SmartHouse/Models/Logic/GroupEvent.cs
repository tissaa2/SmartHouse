using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;

namespace SmartHouse.Models.Logic
{
    public class GroupEvent: Event
    {
        public byte GroupID { get; set; }
        public byte CategoryID { get; set; }
        public byte TimePar { get; set; }

        //private byte groupID = 0;
        //public byte GroupID
        //{
        //    get => groupID;
        //    set
        //    {
        //        CheckIsDirty(groupID, value, "GroupID", () => { groupID = value; });
        //    }
        //}

        //private byte categoryID = 0;
        //public byte CategoryID
        //{
        //    get => categoryID;
        //    set
        //    {
        //        CheckIsDirty(categoryID, value, "CategoryID", () => { categoryID = value; });
        //    }
        //}

        //private byte timePar = 0;
        //public byte TimePar
        //{
        //    get => timePar;
        //    set
        //    {
        //        CheckIsDirty(timePar, value, "TimePar", () => { timePar = value; });
        //    }
        //}

        public GroupEvent()
        {

        }

        public GroupEvent(byte inputId, byte groupId, byte categoryId, byte timePar)
        {
            InputID = inputId;
            GroupID = groupId;
            CategoryID = categoryId;
            TimePar = timePar;
            Init();
        }

        public override UID GetUID(Group group)
        {
            return new UID(0, 0, GroupID);
        }
    }
}
