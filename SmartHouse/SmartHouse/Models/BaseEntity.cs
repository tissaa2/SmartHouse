using System;
using System.Collections.Generic;
using System.Text;
using SmartHouse.Services;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Xml;
using System.IO;
using SmartHouse.Models.Logic;
using SmartHouse.Models.Physics;

namespace SmartHouse.Models
{
    [Serializable]

    public class BaseEntity<IDType>: IUnique<IDType>, INotifyPropertyChanged
    {
        /* private static Dictionary<Type, object> ids = new Dictionary<Type, object>();
        public static IDType NewID<IDType>()
        {
            Type t = typeof(IDType);
            if (ids.ContainsKey(t))
            {
                IConvertible v = (IDType)ids[t] as IConvertible;
                int iv = v.ToInt32(null);
                iv++;
                ids[t] = iv;
                return (IDType)iv;
            }
        } */

        public static IDGenerator<int> IntID = new IDGenerator<int>((v) => { return (int)v + 1; });

        public static explicit operator string (BaseEntity<IDType> e)
        {
            return String.Format("(ID={0}, Sec={1})", e.ID, e.SecurityLevel);
        }

        public Boolean IsAdmin { get { return Settings.IsAdmin; } }
        public Boolean NotIsAdmin { get { return !IsAdmin; } }

        // [XmlAttribute("ID")]
        public IDType ID { get; set; }
        // [XmlAttribute("SecurityLevel")]
        public byte SecurityLevel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //public static T Load<T>(string fileName) where T : class
        //{
        //    try
        //    {
        //        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //        string fn = Path.Combine(path, fileName);

        //        using (StreamReader reader = new StreamReader(new FileStream(fn, FileMode.Open)))
        //        {
        //            XmlSerializer deserializer = new XmlSerializer(typeof(T));
        //            return deserializer.Deserialize(reader) as T;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex);
        //    }
        //    return null;
        //}

        //public virtual void Save(string fileName)
        //{

        //    // string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //    string fn = Path.Combine(path, fileName);

        //    //  using (StreamWriter writer = new StreamWriter(new FileStream(fn, FileMode.OpenOrCreate)))
        //    {
        //        // Scene e = Scene.BrightLight(12);
        //        // GroupEvent e = new GroupEvent(3, 5, 7, 11);
        //        Event e = new Event();
        //        // BaseEntity<int> e = new BaseEntity<int>();
        //        XmlSerializer serializer = new XmlSerializer(e.GetType());
        //        // XmlSerializer serializer = new XmlSerializer(this.GetType());
        //        using (StringWriter textWriter = new StringWriter())
        //        {
        //            // serializer.Serialize(textWriter, this);
        //            serializer.Serialize(textWriter, e);
        //            String s = textWriter.ToString();
        //        }
        //        // serializer.Serialize(writer, this);
        //    }

        //}


        public static T Load<T>(string fileName) where T : class
        {
            T r = null;
            return r;
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string fn = Path.Combine(path, fileName);
                string data = File.ReadAllText(fn);

                r = JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, MissingMemberHandling = MissingMemberHandling.Error });

            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return r;
        }

        public virtual void Save(string fileName)
        {

            // string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fn = Path.Combine(path, fileName);
            string data = JsonConvert.SerializeObject(this, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.None });
            File.WriteAllText(fn, data);
        }

    }
}
