using Newtonsoft.Json;

namespace SmartHouse.Models.Storage
{
    public class IconNamedEntity : NamedEntity
    {
        [JsonProperty(PropertyName = "Icon")]
        public virtual string Icon { get; set; }

        public IconNamedEntity()
        {

        }

        public IconNamedEntity(int id, string nameTemplate, string icon) : base(id, nameTemplate)
        {
            Icon = icon;
        }

    }
}
