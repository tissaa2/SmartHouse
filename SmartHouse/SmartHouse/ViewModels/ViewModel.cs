using System.ComponentModel;
using System;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

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

        private bool isDirty = false;
        public bool IsDirty
        {
            get
            {
                return isDirty;
                // return (Project)GetValue(SelectedItemProperty);
            }

            set
            {
                OnPropertyChanging("IsDirty");
                isDirty = value;
                OnPropertyChanged("IsDirty");
            }
        }

        public Boolean IsAdmin { get { return SmartHouse.Models.Settings.Instance.IsAdmin; } }
        public Boolean NotIsAdmin { get { return !IsAdmin; } }

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


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }


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
