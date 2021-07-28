using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using SmartHouse.Models.Physics;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SmartHouse.Services;
using Java.Util.Zip;

namespace SmartHouse.Models.Storage
{
    // public class ProjectsList : IconListEntity<int, int, Project>
    public class ProjectsList : IconNamedEntity
    {
        public IDGenerator<int> IntID = new IDGenerator<int>((v) => { return (int)v + 1; });

        public Dictionary<int, Project> Projects { get; set; }

        public static string FileName { get; set; } = "projects.json";
        public static ProjectsList TestData {
            get {
                var p = Project.Create("Офис", "project_flat.png", ProjectsList.Instance.IntID.NewID());
                return new ProjectsList()
                {
                    Projects = new Dictionary<int, Project>() { {p.ID, p } }
                };
            }
        }

        public static void LoadTestData()
        {
            instance = TestData;
        }

        private static ProjectsList instance = null;
        private static bool initializing = false;
        public static ProjectsList Instance
        {
            get
            {
                if (instance == null)
                {
                    initializing = true;
                    instance = Load<ProjectsList>(FileName);
                    if (instance == null)
                    {
                        LoadTestData();
                        instance.Save();
                    }
                    initializing = false;
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }
        public bool IsDirty { get; set; } = false;


        public ProjectsList()
        {

        }

        public void Save()
        {
            Save(FileName);
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

                r = JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings()
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
            return r;
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
