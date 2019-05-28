using System.Xml.Serialization;

namespace SmartHouse.Models.Logic
{
    [XmlInclude(typeof(GroupEvent))]
    [XmlInclude(typeof(UIDEvent))]
    public class Event: BaseEntity<int>
    {

        //2do: сделать заполнение параметров входов из эвентлв сцен в устройстве
        public byte TypeID { get; set; } = 5;
        public byte InputID { get; set; }
    }
}
