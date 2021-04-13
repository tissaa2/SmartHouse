using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace SmartHouse.Models
{

    public class ResourceEntry
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    [Serializable]
    public class PhotoPickerModel: INotifyPropertyChanged
    {
        public ObservableCollection<string> SourceTypes { get; set; } = new ObservableCollection<string>() { "Галерея", "Камера", "Встроенные" };

        private ObservableCollection<ResourceEntry> images = new ObservableCollection<ResourceEntry>() {
            new ResourceEntry() { Icon = "gallery.png", Name = "Галерея" },
            new ResourceEntry() { Icon = "camera.png", Name = "Камера" }
        };

        public ObservableCollection<ResourceEntry> Images {
            get {
                return images;
            }
            set {
                images = value;
                OnPropertyChanged("Images");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static string ExtractName(string text, string prefix)
        {
            string s = text.Substring(prefix.Length);
            int i = text.IndexOf(".");
            if (i > 0)
            {
                s = s.Substring(0, i);
            }
            return s;
        }

        public void LoadResources(string prefix)
        {
            Type t = typeof(SmartHouse.Droid.Resource.Drawable);
            var fis = t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (var e in fis)
                if (e.Name.StartsWith(prefix))
                    images.Add(new ResourceEntry() { Icon = e.Name + ".png", Name = ExtractName(e.Name, prefix) });
        }



        public PhotoPickerModel(string prefix)
        {
            LoadResources(prefix);
        }

    }
}
