using Newtonsoft.Json;
using SmartHouse.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace SmartHouse.Models
{
    [Serializable]

    public class BaseObject: INotifyPropertyChanged
    {
        public static List<EntityInfo> GetInheritors(Type parent)
        {
            List<EntityInfo> r = new List<EntityInfo>();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
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
            string data = JsonConvert.SerializeObject(this, new JsonSerializerSettings() {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new JsonConverter[] { new IntToUIDConverter() }
            });
            File.WriteAllText(fn, data);
        }
    }
}
