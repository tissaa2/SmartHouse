using System.Xml.Serialization;

namespace SmartHouse.Models.Logic
{
    [XmlInclude(typeof(GroupEvent))]
    [XmlInclude(typeof(UIDEvent))]
    // public class Event : BaseEntity<int>
    public class Event : BaseEntity
    {
        //2do: сделать заполнение параметров входов из эвентов сцен в устройстве
        private byte typeID = 5;
        public byte TypeID
        {
            get { return typeID; }
            set
            {
                CheckIsDirty(typeID, value, "TypeID", () => { typeID = value; });
            }
        }


        private byte inputID;
        public byte InputID
        {
            get { return inputID; }
            set
            {
                CheckIsDirty(inputID, value, "InputID", () => { inputID = value; });
            }
        }

        public Event()
        {

        }

        public Event(int id) : base(id)
        {

        }

        public virtual UID GetUID(Group group)
        {
            return default(UID);
        }
    }
}
