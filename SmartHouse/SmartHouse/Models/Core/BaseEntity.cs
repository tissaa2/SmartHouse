using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace SmartHouse.Models.Core
{
    public class BaseEntity
    {
        public string Name { get; set; }
        public byte SecurityLevel { get; set; }
        public string Icon { get; set; }

        public static T Load<T>(string fileName) where T : BaseEntity
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(T));
                    return deserializer.Deserialize(reader) as T;
                }
            }
            catch(Exception ex)
            {
                Log.Write(ex);
            }
            return null;
        }

        public void Save(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
            }
        }

    }
}
