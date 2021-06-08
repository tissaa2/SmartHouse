using System.Xml.Serialization;

namespace SmartHouse.Models.Logic
{
    public enum EventType
    {
        UIDEvent,
        GroupEvent
    }

    public class Event
    {
        //2do: сделать заполнение параметров входов из эвентов сцен в устройстве
        public EventType Type { get; set; }

        #region GroupEvent
        public byte CategoryID { get; set; }
        public byte TimePar { get; set; }
        #endregion

        // Если Type == GroupEvent, то первый байт содержит ID группы
        public UID UID { get; set; }
        
        public byte InputTypeID { get; set; } = 5;

        public byte InputID { get; set; }

        public Event()
        {

        }

        public static Event GroupEvent(byte inputId, byte groupId, byte categoryId, byte timePar)
        {
            return new Event()
            {
                InputID = inputId,
                UID = new UID(0, 0, groupId),
                CategoryID = categoryId,
                TimePar = timePar
            };
        }
    }
}
