using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    // public class IconEntity<T> : NamedEntity<T>
    public class IconEntity : NamedEntity
    {
        // [XmlIgnore]
        private string icon;

        // [XmlAttribute("Icon")]
        [JsonProperty(PropertyName = "Icon")]
        public virtual string Icon
        {
            get { return icon; }
            set {
                // icon = value; OnPropertyChanged("Icon");
                CheckIsDirty(icon, value, "Icon", () => { icon = value; }); 
            }
        }


        public IconEntity()
        {

        }

        // public IconEntity(T id, string nameTemplate, string icon) : base(id, nameTemplate)
        public IconEntity(int id, string nameTemplate, string icon) : base(id, nameTemplate)
        {
            Icon = icon;
        }

    }
}
