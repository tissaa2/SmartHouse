using System.ComponentModel;
using System;
using Xamarin.Forms;

namespace SmartHouse.ViewModels
{
    public class ViewModel : BindableObject, INotifyPropertyChanged
    {
        public static object CreateModel(object target)
        {
            Type t = target.GetType();
            var tn = t.Name;
            Type rt = Type.GetType("SmartHouse.ViewModels." + tn + "Model");
            var m = Activator.CreateInstance(rt) as ViewModel;
            m.target = target;
            m.Setup(t);
            return m;
        }

        private object target;
        public object Target
        {
            get
            {
                return target;
            }
            set
            {
                OnPropertyChanging("Target");
                target = value;
                OnPropertyChanged("Target");
            }
        }

        /* public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OnPropertyChanging(string name)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
        } */

        public virtual void Setup(params object[] args)
        {

        }

        public virtual void Assign(ViewModel source)
        {
            this.target = source.target;
        }

        public virtual ViewModel Clone()
        {
            
            throw new Exception("Not implemented");
        }

        public ViewModel()
        {

        }


    }
}
