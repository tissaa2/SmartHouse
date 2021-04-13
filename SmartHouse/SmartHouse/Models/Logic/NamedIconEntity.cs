using Newtonsoft.Json;

namespace SmartHouse.Models.Logic
{
    public class NamedIconEntity : NamedEntity
    {
        [JsonProperty(PropertyName = "Icon")]
        public virtual string Icon { get; set; }

        public NamedIconEntity()
        {

        }

        public NamedIconEntity(int id, string nameTemplate, string icon) : base(id, nameTemplate)
        {
            Icon = icon;
        }

    }
}
