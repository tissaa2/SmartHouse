using Newtonsoft.Json;
using SmartHouse.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using Java.Util.Zip;
using System.Linq;
using Xamarin.Forms;
using SmartHouse.Models.Logic;

namespace SmartHouse.Models
{
    [Serializable]

    public class BaseObject: INotifyPropertyChanged
    {
        public static List<EntityInfo> GetInheritors(Type parent, Type[] exclude)
        {
            List<EntityInfo> r = new List<EntityInfo>();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (exclude != null)
                    if (exclude.FirstOrDefault(e => e.Name == t.Name) != null)
                        continue;

                if (t.IsSubclassOf(parent))
                {
                    var a = t.GetCustomAttribute<IconNameAttribute>();
                    if (a != null)
                    {
                        var hash = a.Name.GetHashCode();
                        r.Add(new EntityInfo() { Props = a, Type = t, ID = hash });
                    }
                }
            }
            return r;
        }

        public delegate void ParameterlessDelegate();
        protected virtual object CheckIsDirty(object oldValue, object newValue, string eventName, ParameterlessDelegate setter)
        {
            if (Object.Equals(oldValue, newValue))
                return oldValue;
            else
            {
                setter?.Invoke();
                if (Initialized)
                {
                    ProjectsList.SetIsDirty(true);
                }
                OnPropertyChanged(eventName);
                return newValue;
            }
        }

        [JsonIgnore]
        public bool Initialized { get; set; } = false;

        private event PropertyChangedEventHandler propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                propertyChanged += value;
            }

            remove
            {
                propertyChanged -= value;
            }
        }

        public void OnPropertyChanged(string name)
        {
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual void Init()
        {
            Initialized = true;
        }

        public static T Load<T>(string fileName) where T : class
        {
            T r = null;
            // return r;
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string fn = Path.Combine(path, fileName);
                string data = File.ReadAllText(fn);

                r = JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings() {
                    TypeNameHandling = TypeNameHandling.All,
                    MissingMemberHandling = MissingMemberHandling.Error,
                    Converters = new JsonConverter[] { new IntToUIDConverter() }
                });

            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return r;
        }

        public BaseObject()
        {

        }

        public virtual void Save(string fileName)
        {
            // string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fn = Path.Combine(path, fileName);
            string data = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new JsonConverter[] { new IntToUIDConverter() }
            });
            File.WriteAllText(fn, data);
        }

        public virtual byte[] Zip()
        {
            string data = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new JsonConverter[] { new IntToUIDConverter() }
            });
            var d = new Deflater();
            var bts = Encoding.Unicode.GetBytes(data);
            d.SetInput(bts);
            d.Finish();
            byte[] buf = new byte[UInt16.MaxValue];
            int size = d.Deflate(buf);
            d.End();
            byte[] result = new byte[size];
            Array.Copy(buf, result, size);

            // var i = new Inflater();
            // i.SetInput(result);
            // int size0 = i.Inflate(buf);
            // var s = Encoding.Unicode.GetString(buf, 0, size0);
            return result;
        }

        public static T UnZip<T>(byte[] data)
        {
            T result = default(T);
            try
            {
                byte[] buf = new byte[UInt16.MaxValue * 4];
                var i = new Inflater();
                i.SetInput(data);
                int size = i.Inflate(buf);
                var s = Encoding.Unicode.GetString(buf, 0, size);
                result = JsonConvert.DeserializeObject<T>(s, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                    MissingMemberHandling = MissingMemberHandling.Error,
                    Converters = new JsonConverter[] { new IntToUIDConverter() }
                });

            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
            return result;
        }

    }
}
