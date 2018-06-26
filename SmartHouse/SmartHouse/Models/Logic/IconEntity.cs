using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    public class IconEntity<T> : NamedEntity<T>
    {
        // [XmlIgnore]
        private string icon;

        // [XmlAttribute("Icon")]
        [JsonProperty(PropertyName = "Icon")]
        public virtual string Icon
        {
            get { return icon; }
            set { icon = value; OnPropertyChanged("Icon"); }
        }


    }
}
