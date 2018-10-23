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
        public static IDGenerator<UID> UIDID = new IDGenerator<UID>((v) => { return new UID((int)v + 1); });

        public static explicit operator string (BaseEntity<IDType> e)
        {
            return String.Format("(ID={0}, Sec={1})", e.ID, e.SecurityLevel);
        }

        public Boolean IsAdmin { get { return Settings.IsAdmin; } }
        public Boolean NotIsAdmin { get { return !IsAdmin; } }

        private IDType id;

        [JsonProperty(PropertyName = "ID")]
        public virtual IDType ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged("ID"); }
        }

        private byte securityLevel;

        [JsonProperty(PropertyName = "SecurityLevel")]
        public virtual byte SecurityLevel
        {
            get { return securityLevel; }
            set { securityLevel = value; OnPropertyChanged("SecurityLevel"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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

        public BaseEntity()
        {

        }

        public BaseEntity(IDType id)
        {
            this.ID = id;
        }

        public virtual void Save(string fileName)
        {

            // string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fn = Path.Combine(path, fileName);
            string data = JsonConvert.SerializeObject(this, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.None });
            File.WriteAllText(fn, data);
        }

        public virtual void Assign(BaseEntity<IDType> source)
        {
            this.ID = source.ID;
            this.SecurityLevel = source.SecurityLevel;
        }

        public virtual BaseEntity<IDType> Clone()
        {
            throw new Exception("Not implemented");
        }

    }
}
