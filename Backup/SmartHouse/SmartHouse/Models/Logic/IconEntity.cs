using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    // public class IconEntity<T> : NamedEntity<T>
    public class IconEntity : NamedEntity
    {
        [JsonProperty(PropertyName = "Icon")]
        public virtual string Icon { get; set; }

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
