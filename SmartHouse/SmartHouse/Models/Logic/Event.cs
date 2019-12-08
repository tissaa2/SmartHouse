using System.Xml.Serialization;

namespace SmartHouse.Models.Logic
{
    [XmlInclude(typeof(GroupEvent))]
    [XmlInclude(typeof(UIDEvent))]
    public class Event : BaseEntity<int>
    {


        //2do: сделать заполнение параметров входов из эвентлв сцен в устройстве
        public byte TypeID { get; set; } = 5;
        public byte InputID { get; set; }

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
